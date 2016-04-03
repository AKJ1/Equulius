namespace Assets.Scripts.Game
{
    using UnityEngine;

    public class PropJitter : MonoBehaviour
    {
        [Range(25f, 20000f)]
        public float ScalePercentCeil;

        [Range(25f, 20000f)]
        public float ScalePercentFloor;

        public Vector3 RotationRngCeil;
        public Vector3 RotationRngBot;

        public Vector3 PositionJitterCeil;
        public Vector3 PositionJitterFloor;

        public void Jitter()
        {
            JitterPosition();
            JitterRotation();
            JitterScale();
        }

        public void JitterRotation()
        {
            transform.Rotate(Vector3.up * Random.Range(RotationRngBot.y, RotationRngCeil.y), Space.Self);
            transform.Rotate(Vector3.forward * Random.Range(RotationRngBot.z, RotationRngCeil.z), Space.Self);
            transform.Rotate(Vector3.right * Random.Range(RotationRngBot.x, RotationRngCeil.x), Space.Self);
        }

        public void JitterPosition()
        {
            transform.Translate(transform.TransformDirection(new Vector3(
                  Random.Range(PositionJitterFloor.x, PositionJitterCeil.x)
                , Random.Range(PositionJitterFloor.y, PositionJitterCeil.y)
                , Random.Range(PositionJitterFloor.z, PositionJitterCeil.z))));
            
        }

        public void JitterScale()
        {
            float rng = Random.Range(ScalePercentFloor, ScalePercentCeil);
            transform.localScale *= (rng/100);
        }
    }
}