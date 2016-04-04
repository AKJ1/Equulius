using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PropSpacer : MonoBehaviour
    {
        public void OnTriggerStay(Collider other)
        {
//            Debug.Log("SPACING!!");
            if (StaticData.InsaneMode)
            {
                if (other.transform.tag == "PlanetaryProp")
                {
                    transform.Translate((transform.position - other.ClosestPointOnBounds(transform.position)).normalized);
                }
            }
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag == "PlanetaryProp")
            {
//                transform.Translate((transform.position - other.(transform.position)).normalized*3);
            }
        }
        
    }
}