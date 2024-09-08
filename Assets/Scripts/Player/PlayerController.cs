using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f;
        public float mouseSensitivity = 100f;
    
        private CharacterController characterController;
        private Camera playerCamera;
        private float xRotation = 0f;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Move();
            RotateView();
        }

        void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * speed * Time.deltaTime);
        }

        void RotateView()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}