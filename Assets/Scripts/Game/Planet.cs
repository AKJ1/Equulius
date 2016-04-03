using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Planet : MonoBehaviour
    {
        public SphereCollider AtmosphereCollider;

        public SphereCollider GroundCollider;

        public float GroundRadius { get { return GroundCollider.radius; } }

        public float AtmosphereRadius { get { return GroundRadius * 2f; } }

        public void RotateAwayFromPlanet(GameObject go, PlanetAttachmentType type)
        {
            if (type == PlanetAttachmentType.Ground)
            {
                go.transform.LookAt(go.transform.position + (go.transform.position - transform.position).normalized);
            }
            else
            {
                go.transform.LookAt(transform.position);
            }
        }

        public void PositionOnPlanetSurface(GameObject go, PlanetAttachmentType type)
        {
            float offset = type == PlanetAttachmentType.Atmospheric
                ? AtmosphereRadius
                : GroundRadius;

            offset *= transform.localScale.x;

            if (go.GetComponent<PlanetaryProp>() != null)
            {
                offset += go.GetComponent<PlanetaryProp>().DistanceOffset;
            }
            go.transform.position = (go.transform.position - transform.position).normalized * (offset);
        }
        
    }

    
}