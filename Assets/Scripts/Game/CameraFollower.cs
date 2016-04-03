namespace Assets.Scripts.Game
{
    using UnityEngine;
    public class CameraFollower : MonoBehaviour
    {
        public Planet OrbitedPlanet;

        public GameObject Target;

        public float StretchThreshold;

        public float CompressionThreshold;

        public Vector2 Offset;

        private float stretch;

        public float CameraHeight;

        public float MinXAxis;

        [Range(1,20)]
        public float CameraFirmness;


        private Rigidbody cameraBody;
        // Use this for initialization
        void Start()
        {
            cameraBody = transform.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            HandleView();
        }
        

        void HandleView()
        {
            transform.LookAt(Target.transform);

//            transform.position = Target.transform.position;


            Vector3 desiredXZPos = Target.transform.position - (Target.transform.forward*StretchThreshold);
            transform.position = Vector3.Lerp(transform.position, desiredXZPos, Time.deltaTime * CameraFirmness);
            Vector3 desiredYPos = Target.transform.position + (Target.transform.up*CameraHeight);
            transform.position = Vector3.Lerp(transform.position, desiredYPos, Time.deltaTime * CameraFirmness);


            var rotation = Quaternion.Euler(transform.eulerAngles);
            rotation.SetLookRotation(Target.transform.position - transform.position , Target.transform.up);
            transform.localRotation = rotation;
//            transform.rotation.SetLookRotation(Target.transform.forward, Target.transform.up);
        }
    }
}
