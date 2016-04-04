using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PlanetaryExterminator : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlanetaryProp")
            {
                other.transform.parent.GetComponent<PoolMember>().Return();
            }
        }

//
        public void OnCollisionEnter(Collision col)
        {
            Debug.Log("ENTERED COLLISION");
        }
    }
}