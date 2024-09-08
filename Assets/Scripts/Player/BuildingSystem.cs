using Player;
using Player.Data;
using UnityEngine;
public class BuildingSystem : MonoBehaviour
{
    public Transform playerCamera;
    public LayerMask placementMask; // Слой, на котором можно размещать объекты (placmentBuild)

    public float placementDistance = 40f; 
    public float maxObjectSelectionDistance = 40f;
    
    private bool isBuildingMode = false;
    private GameObject currentObject;
    private PlacementType currentObjectType;
    private float currentRotation = 0f;

    private Color validColor = Color.green;
    private Color invalidColor = Color.red;
    private Color previewColor = new Color(1, 1, 1, 0.5f);
    
    private Vector3 objectSize;
    private RaycastHit _hit;
    
    void Update()
    {
        if (isBuildingMode)
        {
            UpdateObjectPosition();
            RotateObject();
            PlaceObject();
        }
        else
        {
            CheckForBuildMode();
        }
    }
    void CheckForBuildMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out _hit, maxObjectSelectionDistance))
            {
                var buildItem = _hit.collider.gameObject.GetComponent<BuildItem>();
                if(buildItem == null) return;
                
                currentObjectType = buildItem.PlacementType;
                isBuildingMode = true;
                currentObject = _hit.collider.gameObject;
                objectSize = currentObject.GetComponent<Collider>().bounds.size; 
                SelectObject(currentObject);
            }
        }
    }
    void SelectObject(GameObject obj)
    {
        Collider objectCollider = obj.GetComponent<Collider>();
        if (objectCollider != null)
        {
            //objectCollider.enabled = false; 
            obj.layer = 1;
        }

        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            previewColor = renderer.material.color;
        }

        IsValidPlacement(obj.transform.position);
    }
    void UpdateObjectPosition()
    {
        if (currentObject == null) return;
        
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out _hit, placementDistance, placementMask))
        {
            currentObject.transform.position = _hit.point + OffsetPosition(); 
            currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hit.normal);
            IsValidPlacement(currentObject.transform.position);
        }
    }

    private Vector3 OffsetPosition()
    {
        var wallCenter = _hit.point + _hit.normal * objectSize.x / 2;
        return wallCenter - _hit.point;
    }

    bool IsValidPlacement(Vector3 position)
    {
        if (!_hit.collider.CompareTag(currentObjectType.ToString())) 
        {
            SetPlacementIndicatorColor(false);
            return false;
        }

        Collider[] colliders = Physics.OverlapBox(
            position + _hit.normal, 
            objectSize / 2, 
            currentObject.transform.rotation
        );

        foreach (var collider in colliders)
        {
            var name = currentObjectType.ToString();
            if (collider.gameObject != currentObject && !collider.CompareTag("Player"))
            {
                SetPlacementIndicatorColor(false);
                return false;
            }
        }
        
        SetPlacementIndicatorColor(true);
        return true;
    }

    private void SetPlacementIndicatorColor(bool isValid)
    {
        validColor.a = 0.5f;
        invalidColor.a = 0.5f;
        currentObject.GetComponent<Renderer>().material.color = isValid ? validColor : invalidColor;
    }

    void RotateObject()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentRotation += 45f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentRotation -= 45f;
        }
        currentObject.transform.Rotate(Vector3.up, currentRotation);
    }
    
    void PlaceObject()
    {
        if (Input.GetMouseButtonDown(0) && IsValidPlacement(currentObject.transform.position))
        {
            currentObject.GetComponent<Renderer>().material.color = previewColor;
            var objectCollider = currentObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
                currentObject.layer = 3;
            }
            isBuildingMode = false;
            currentObject = null;
        }
    }
}