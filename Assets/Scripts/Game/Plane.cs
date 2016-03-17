namespace Assets.Scripts.Game
{
    using UnityEngine; 
    using UnityEditor;

    public class Plane : MonoBehaviour
    {
        public float MoveSpeedZ;
        public float MoveSpeedX;
        public float MoveSppedY;

        public Vector2 MoveVector;
        public GameObject Planet;

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
            
            transform.position = -planeDirection * 160;
            Vector3 surfacePoint = transform.position ; // normal

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

//            Debug.Log(tangent);
            Debug.DrawRay(transform.position, tangent, Color.red);
            float tangentControlAngle = Mathf.Tan(surfacePoint.normalized.x) * Mathf.Rad2Deg;

            if (Mathf.Abs(MoveVector.x) > 0)
            {
                float max = Mathf.PI*35*Mathf.Deg2Rad;
                float curr = Mathf.Deg2Rad*tangentControlAngle;
                float num = Mathf.Pow(Mathf.PI, curr/max);
                


                rb.AddForce(transform.right*50 * MoveVector.x);
            }
            
            float tangentAngle = Mathf.Atan2(surfacePoint.normalized.z, surfacePoint.normalized.y)*Mathf.Rad2Deg;


            Vector3 bodyForwardRotation = transform.forward;
            bodyForwardRotation.y = 0;
            var desiredRotation = Quaternion.Euler(tangentAngle, 0, Mathf.Clamp(-tangentControlAngle,-35f, 35f));
            var desiredQuatLookRot = Quaternion.LookRotation(Vector3.forward)*desiredRotation;
            transform.rotation = desiredQuatLookRot;
            rb.AddForce(transform.forward * 40);
        }

        public void MoveXy()
        {
        }
        
    }
}