namespace Assets.Scripts.Game
{
    using UnityEngine;
    public class CameraFollower : MonoBehaviour
    {
        public GameObject Target;

        public float StretchThreshold;

        public float CompressionThreshold;

        public Vector2 Offset;

        private float stretch;

        public float CameraHeight;

        public float MinXAxis;

        public float Decay;

        private Rigidbody cameraBody;
        // Use this for initialization
        void Start()
        {
            cameraBody = transform.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            UpdateSpring();
            ApplyForce();
            HandleView();
            transform.LookAt(Target.transform);
        }
#if DEVELOPMENT_BUILD || UNITY_EDITOR

        private string dbgstr = "";

    #endif         
        void UpdateSpring()
        {
            float diff = (transform.position - Target.transform.position).magnitude;
            dbgstr = diff.ToString();
            if (Mathf.Abs(stretch) > 0)
            {
                float sign = Mathf.Sign(stretch);
                float abStr = Mathf.Abs(stretch) - Mathf.Abs(stretch * Decay);

                stretch = abStr * sign;

                if (Mathf.Abs(stretch) < .005f)
                {
                    stretch = 0;
                }
            }
            if (diff > StretchThreshold)
            {
                stretch += diff - StretchThreshold;

            }

            else if (CompressionThreshold > diff)
            {
                stretch -= CompressionThreshold - diff;
            }
        }

        void ApplyForce()
        {
            cameraBody.velocity = ((Target.transform.position - transform.position).normalized * stretch);
        }

        void HandleView()
        {
            Quaternion originalQt = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(transform.rotation.eulerAngles.x, Target.transform.localRotation.eulerAngles.y, transform.rotation.eulerAngles.z), .5f);
            transform.LookAt(Target.transform);
            transform.rotation = Quaternion.Lerp(originalQt, Quaternion.Slerp(transform.rotation, Quaternion.Euler(5, Target.transform.localRotation.eulerAngles.y, 0), .5f), .23f);

            var desiredPositon = Target.transform.localPosition - (Target.transform.forward * StretchThreshold) + new Vector3(0, CameraHeight, 0);
            transform.position = Vector3.Lerp(transform.position, desiredPositon, 0.2f);
        }
    }
}
