using System.Collections;
using UnityEditor;

namespace Assets.Scripts.Game
{
    using UnityEngine;

    public class PlanetaryProp : MonoBehaviour
    {
        public Planet PlanetParent;

        public float DistanceOffset;

        public PlanetAttachmentType AttachmentType;

        public PropType Type;

        public TreeDecorator TreeDecorator;

        public bool WithChildren;

        private bool canSpawn = false;

        public void Start()
        {
            canSpawn = true;
        }

        IEnumerator TimeOutSpawn()
        {
            while (!canSpawn)
            {
                yield return new WaitForEndOfFrame();
            }
            OnSpawn();
        }

        public void OnSpawn()
        {
            
            if (!canSpawn)
            {
                StartCoroutine(TimeOutSpawn());
                return;
            }
            RotateAwayFromPlanet(gameObject);
            PositionOnPlanetSurface(gameObject);


            if (WithChildren)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
//                    Debug.Log("CHILD!!!");
//                    RotateAwayFromPlanet(transform.GetChild(i).gameObject);
//                    PositionOnPlanetSurface(transform.GetChild(i).gameObject);
                    var childProp = transform.GetChild(i).GetComponent<PlanetaryProp>();
                    childProp.PlanetParent = PlanetParent;
                    childProp.OnSpawn();
                }
            }
            
            var jitter = gameObject.GetComponent<PropJitter>();
            if (jitter != null)
            {
                jitter.Jitter();
            }

            var treeDeco = transform.GetComponent<TreeDecorator>();
            if (treeDeco != null)
            {
                treeDeco.Decorate();
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

            if (go.GetComponent<PlanetaryProp>() != null)
            {

                offset += go.GetComponent<PlanetaryProp>().DistanceOffset;
            }
            go.transform.position = ( go.transform.position - PlanetParent.transform.position).normalized * (offset);
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