namespace Assets.Scripts.Game
{
    using UnityEngine; 

    public class Plane : MonoBehaviour
    {
        public float MoveSpeedZ;
        public float MoveSpeedX;
        public float MoveSppedY;

        public float PlaneHeight;

        public Vector2 MoveVector;
        public GameObject Planet;   
        public GameObject PlaneModel;

        private Rigidbody rb;

        public void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
        }

        public void Update()
        {
            HandleInput();
            Fly();
            MoveXy();
        }

        private void HandleInput()
        {
            MoveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public void Fly()
        {
            Vector3 planeDirection = (Planet.transform.position - transform.position).normalized;
            
            transform.position = -planeDirection * PlaneHeight;
            Vector3 surfacePoint = transform.position ; // normal

            Vector3 forward = new Vector3(0,1,1);
            Vector3 tangent;
            Vector3 t1 = Vector3.Cross(surfacePoint, Vector3.forward);
            Vector3 t2 = Vector3.Cross(surfacePoint, Vector3.up);
            if (t1.magnitude > t2.magnitude)
            {
                tangent = t1;
            }
            else
            {
                tangent = t2;
            } 
            
            float tangentControlAngle = Mathf.Tan(surfacePoint.normalized.x) * Mathf.Rad2Deg;

            if (Mathf.Abs(MoveVector.x) > 0)
            {
                float max = Mathf.PI*35*Mathf.Deg2Rad;
                float curr = Mathf.Deg2Rad*tangentControlAngle;
                float num = Mathf.Pow(Mathf.PI, curr/max);
            
                
//                rb.velocity = Vector3.Lerp(rb.velocity, transform.right * MoveSpeedX * MoveVector.x, .2f);
            }

            
            float tangentAngle = Mathf.Atan2(surfacePoint.normalized.z, surfacePoint.normalized.y)*Mathf.Rad2Deg;


            Vector3 bodyForwardRotation = transform.forward;
            bodyForwardRotation.y = 0;
            Debug.Log(Mathf.Cos(Mathf.Deg2Rad * tangentAngle));
            var desiredRotation = Quaternion.Euler(tangentAngle , MoveVector.x * 35,  ( MoveVector.x * 35 )+ Mathf.Clamp(-tangentControlAngle,-35f, 35f));
            var newDesiredRotation = Quaternion.LookRotation(transform.forward + new Vector3(MoveVector.x ,0,0));
            newDesiredRotation = Quaternion.Euler(tangentAngle, newDesiredRotation.eulerAngles.y, Mathf.Clamp(-tangentControlAngle, -35f, 35f));
            
            transform.localRotation = desiredRotation;
//            if (useGUILayout)
//            {
//            var axis = Quaternion.LookRotation(transform.forward , transform.up);
//            transform.localRotation= Quaternion.Euler(axis.eulerAngles.SubvectorZ() + axis.eulerAngles.SubvectorY() + transform.eulerAngles);
            
            

//            Debug.Log(rb.velocity.SubvectorX().magnitude);
                
//            }

//            transform.localRotation = Quaternion.LookRotation(-transform.forward, -transform.up);
//            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

            rb.velocity =  (transform.forward * MoveSpeedZ);
        }

        public void MoveXy()
        {
        }
        
    }
}