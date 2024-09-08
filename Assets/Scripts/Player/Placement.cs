using Player.Data;
using UnityEngine;

namespace Player
{
    public class Placement : MonoBehaviour
    {
        [SerializeField] private PlacementType _type = PlacementType.Floor;

        public PlacementType PlacementType => _type;
    }
}