namespace Assets.Scripts.Game
{
    using UnityEngine;

    public class PlanetaryProp : MonoBehaviour
    {
        public Planet PlanetParent;

        public float DistanceOffset;

        public PlanetAttachmentType AttachmentType;

        public PropType Type;

        public bool WithChildren;

        public void Start()
        {
        }

        public void OnSpawn()
        {
            RotateAwayFromPlanet(gameObject);
            PositionOnPlanetSurface(gameObject);

            if (WithChildren)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
//                    Debug.Log("CHILD!!!");
                    RotateAwayFromPlanet(transform.GetChild(i).gameObject);
                    PositionOnPlanetSurface(transform.GetChild(i).gameObject);
                }
            }
        }

        public void RotateAwayFromPlanet(GameObject go)
        {
            if (this.AttachmentType == PlanetAttachmentType.Ground)
            {
                go.transform.LookAt(go.transform.position + (go.transform.position - PlanetParent.transform.position).normalized);
            }
            else
            {
                go.transform.LookAt(PlanetParent.transform.position);

            }
        }

        public void PositionOnPlanetSurface(GameObject go)
        {
            float offset = AttachmentType == PlanetAttachmentType.Atmospheric
                ? PlanetParent.AtmosphereRadius
                : PlanetParent.GroundRadius;
            
            offset *= PlanetParent.transform.localScale.x;

//            Debug.Log(offset);
//            Debug.Log(DistanceOffset);
            go.transform.position = ( go.transform.position - PlanetParent.transform.position).normalized * (offset + go.GetComponent<PlanetaryProp>().DistanceOffset);
        }

        public void ConformChildren()
        {
            
        }

        public void RandomizeScale()
        {
            
        }
    }
}

public enum PlanetAttachmentType
{
    Atmospheric,
    Ground,
}

public enum PropType
{
    Tree,
    Cloud,
    Rock,
    Mountain,
    Obstacle
}