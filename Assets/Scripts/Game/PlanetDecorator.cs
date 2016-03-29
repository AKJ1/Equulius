using System.Linq;
using System.Runtime.Remoting;
using Reaktion;

namespace Assets.Scripts.Game
{
    using UnityEngine;
    using Assets.Scripts.Util;
    using System.Collections.Generic;

    public class PlanetDecorator : MonoBehaviour
    {
        public Planet TargetPlanet;

        public Dictionary<PropType, List<Pool<PlanetaryProp>>> PropPools;

        public List<PlanetaryProp> Props;
        
        public BoxCollider Spawner;

        public BoxCollider Deleter;

        private float spawnTime = .4f;

        private float currentTime = 0f;
        
        // Use this for initialization
        void Start ()
        {
            PropPools = new Dictionary<PropType, List<Pool<PlanetaryProp>>>();
            foreach (PlanetaryProp prop in Props)
            {
                if (PropPools.ContainsKey(prop.Type))
                {
                    PropPools[prop.Type].Add(new Pool<PlanetaryProp>(prop.gameObject, 32, true));
                }
                else
                {
                    PropPools.Add(prop.Type,new List<Pool<PlanetaryProp>>());
                    PropPools[prop.Type].Add(new Pool<PlanetaryProp>(prop.gameObject, 32, true));
                }
            }
        }
	
        // Update is called once per frame
        void Update () {
            if (currentTime <= 0)
            {
                SpawnProp(PropType.Mountain);
                SpawnProp(PropType.Cloud);
                SpawnProp(PropType.Tree);
//                SpawnProp(PropType.Rock);
                currentTime = spawnTime;
            }
            currentTime -= Time.deltaTime;
        }

        void SpawnProp(PropType type)
        {
            int randomPool =Random.Range(0, PropPools[type].Count);
            PlanetaryProp newProp = PropPools[type][randomPool].Get();
            newProp.PlanetParent = TargetPlanet;
            newProp.transform.position = RandomPointOnPlanet(transform.position);
            newProp.AttachmentType = PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().AttachmentType;
            newProp.DistanceOffset = PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().DistanceOffset;
            newProp.WithChildren= PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().WithChildren;
            newProp.OnSpawn();
        }

        Vector3 RandomPointOnPlanet(Vector3 Midpoint)
        {
            Vector3 point1 = TargetPlanet.transform.position + new Vector3(-TargetPlanet.transform.localScale.x / 2, 0, 0);
            Vector3 point2 = TargetPlanet.transform.position + new Vector3(TargetPlanet.transform.localScale.x / 2, 0, 0);
            
            Vector3 finalPoint = Vector3.Lerp(Vector3.Lerp(point1, Midpoint, Random.Range(0f, 1f)), Vector3.Lerp(Midpoint, point2, Random.Range(0f, 1f)), Random.Range(0f, 1f));
            return finalPoint;

        }
    }
}
