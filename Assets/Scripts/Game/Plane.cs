using System;
using System.Collections;
using System.Net.NetworkInformation;

namespace Assets.Scripts.Game
{
    using UnityEngine; 

    public class Plane : MonoBehaviour
    {
        public float MoveSpeedZ;
        public float MoveSpeedX;
        public float MoveSpeedY;

        public float PlaneHeight;
        public float PlaneYFreedom = 50f;
        public float PlaneXFreedom = 35;
        public float StartPlaneHeight;
        private float extraXRotation = 0;
        private float extraYRotation = 0;
        public Vector2 MoveVector;
        public GameObject Planet;   
        public GameObject PlaneModel;
        public GameObject ForwardDirectionHelper;


        private Rigidbody rb;

        public Vector3 SurfacePoint;

        public void Start()
        {
            rb = transform.GetComponent<Rigidbody>();

            Vector3 planeDirection = (Planet.transform.position - transform.position).normalized;

            Vector3 surfacePoint = -planeDirection * PlaneHeight; // normal
            StartPlaneHeight = PlaneHeight;
            transform.position = surfacePoint;
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

        public void ConsumePowerup(PowerupType type)
        {
            switch (type)
            {
                case PowerupType.Speed:
                    break;
                case PowerupType.Score:
                    MoveSpeedZ += 25f;
                    break;
                case PowerupType.Shield:
                    break;
    
            }
        }

        public void Fly()
        {
            Vector3 planeDirection = (Planet.transform.position - transform.position).normalized;

            PlaneHeight += MoveVector.y*MoveSpeedY*Time.deltaTime;
            PlaneHeight = Mathf.Clamp(PlaneHeight, StartPlaneHeight - PlaneYFreedom, StartPlaneHeight + PlaneYFreedom);

            Vector3 surfacePoint = -planeDirection*PlaneHeight; // normal
            SurfacePoint = surfacePoint;

            extraXRotation += -MoveVector.y*PlaneXFreedom*(Time.deltaTime*MoveSpeedY);
            extraXRotation = Mathf.Lerp(extraXRotation, -MoveVector.y*PlaneXFreedom*(Time.deltaTime*MoveSpeedY), Time.deltaTime*5);
            extraXRotation = Mathf.Clamp(extraXRotation, -PlaneXFreedom, PlaneXFreedom);


            extraYRotation += -MoveVector.x*PlaneYFreedom*(Time.deltaTime*MoveSpeedY);
            extraYRotation = Mathf.Lerp(extraYRotation, -MoveVector.x*-PlaneYFreedom*(Time.deltaTime*MoveSpeedY), Time.deltaTime*5);
            extraYRotation = Mathf.Clamp(extraYRotation, -PlaneYFreedom, PlaneYFreedom);

            float tangentControlAngle = Mathf.Tan(surfacePoint.normalized.x)*Mathf.Rad2Deg;
            float tangentAngle = Mathf.Atan2(surfacePoint.normalized.z, surfacePoint.normalized.y)*Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(extraYRotation, planeDirection)*Quaternion.AngleAxis(tangentAngle + extraXRotation, Vector3.right)*Quaternion.AngleAxis(Mathf.Clamp(-tangentControlAngle, -35f, 35f), Vector3.forward);


            Vector3 currentVelocity = rb.velocity;
            Vector3 wantedVelocity = Vector3.Lerp(currentVelocity, transform.forward*MoveSpeedZ, Time.deltaTime*4); // + ((transform.right * MoveSpeedX * MoveVector.x) * Time.deltaTime* 10);

            rb.velocity = wantedVelocity;
        }

        public void OnCollisionEnter(Collision col)
        {
            if (col.transform.tag == "PlanetaryProp")
            {
                Debug.Log("Plane Hit, maytey");
            }
            else if (col.transform.tag == "Powerup")
            {
                ConsumePowerup(col.transform.GetComponent<Powerup>().Type);
                col.transform.GetComponent<Powerup>().enabled = false;
                Destroy(col.transform, 3);
            }
        }

        public void OnTriggerEnter(Collider col)
        {
            Debug.Log(col.transform.name);
            if (col.transform.tag == "Powerup")
            {
                ConsumePowerup(col.transform.GetComponent<Powerup>().Type);
                col.transform.GetComponent<Powerup>().enabled = false;

                StartCoroutine(KillCoin(col.gameObject));
            }
        }

        IEnumerator KillCoin(GameObject coin)
        {
            float animationTime = .3f;
            float elapsed = 0;
            Vector3 scale = coin.transform.localScale;

            while (true)
            {
                elapsed += Time.deltaTime;
                coin.transform.localScale = Vector3.Lerp(scale, Vector3.zero, elapsed/animationTime);
                yield return new WaitForEndOfFrame();
            }
            Destroy(coin);
        }

        public void MoveXy()
        {
        }
    }
}