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
//            transform.LookAt(Target.transform);
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
            Quaternion originalQt = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles.SubvectorXZ()), Quaternion.Euler(new Vector3(Target.transform.rotation.x, Target.transform.rotation.y, Target.transform.rotation.y)), 1f);
            transform.LookAt(Target.transform);
            transform.rotation = Quaternion.Lerp(originalQt, Quaternion.Slerp(Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y)), Quaternion.Euler(Target.transform.rotation.x, 0, 0), 0f), 1f);
            transform.rotation.SetLookRotation(Target.transform.forward, Target.transform.up);
            var desiredPositon = Target.transform.localPosition - (Target.transform.forward * StretchThreshold) + (Target.transform.up *  CameraHeight);
            transform.position = Vector3.Lerp(transform.position, desiredPositon, 0.2f);
        }
    }
}
