using Player.Data;
using UnityEngine;

namespace Player
{
    public class BuildItem : MonoBehaviour
    {
        [SerializeField] private PlacementType _itemType = PlacementType.Floor;

        public PlacementType PlacementType => _itemType;
    }
}