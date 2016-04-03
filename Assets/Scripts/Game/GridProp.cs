using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GridProp : MonoBehaviour
    {
        public float ScaleSizeJitterFloor;
        public float ScaleSizeJitterCeiling;
        
        public Vector3 RotationJitterFloor;
        public Vector3 RotationJitterCeiling;

        public Vector3 PositionJitterFloor;
        public Vector3 PositionJitterCeiling;
    }
}