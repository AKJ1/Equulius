﻿namespace Assets.Scripts.Game
{
    using UnityEngine;
    using Assets.Scripts.Util;
    using System.Collections.Generic;
    using System.Collections;

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
        void Start()
        {
            PropPools = new Dictionary<PropType, List<Pool<PlanetaryProp>>>();
            foreach (PlanetaryProp prop in Props)
            {
                if (PropPools.ContainsKey(prop.Type))
                {
                    PropPools[prop.Type].Add(new Pool<PlanetaryProp>(prop.gameObject, 64, true));
                }
                else
                {
                    PropPools.Add(prop.Type, new List<Pool<PlanetaryProp>>());

                    var newPool = new Pool<PlanetaryProp>(prop.gameObject, 64, true);

                    PropPools[prop.Type].Add(newPool);

                }
            }

//            StartCoroutine(DecorateEntirePlanet());
            SpawnBoudningMountains(800);
        }

        IEnumerator DecorateEntirePlanet()
        {
            int iterations = 48;
            int currIterations = iterations;
            while(currIterations > 0)
            {
                currIterations--;
                SpawnProp(PropType.Mountain);
                SpawnProp(PropType.Cloud);
                SpawnProp(PropType.Tree);
//                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        private float ProgressAmountByIterations(int i)
        {
            return ((Mathf.PI*2*(TargetPlanet.transform.localScale.x/2))/i);
        }

        private void MoveForward(float amt)
        {
            transform.position = transform.position + transform.forward * amt;

            TargetPlanet.PositionOnPlanetSurface(gameObject, PlanetAttachmentType.Ground);

            Vector3 surfacePoint = transform.position; // normal

            float tangentAngle = Mathf.Atan2(surfacePoint.normalized.z, surfacePoint.normalized.y) * Mathf.Rad2Deg;
            float tangentControlAngle = Mathf.Tan(surfacePoint.normalized.x) * Mathf.Rad2Deg;
            var desiredRotation = Quaternion.Euler(tangentAngle, 0, Mathf.Clamp(-tangentControlAngle, -35f, 35f));
            transform.localRotation = desiredRotation;
        }

        private void SpawnBoudningMountains(int iterations = 48)
        {
            float deltaMinPerIteration = .004f;
            float deltaMaxPerIteration = .008f;
            float max = .22f;
            float start = 0;
            float multiplier = 1;

            float currentSide = 1;

            float activeExtent = start;
            for (int i = 0; i < iterations; i++)
            {
                
//                Debug.Log(activeExtent);
                if (Mathf.Abs(activeExtent) >= max)
                {
                    activeExtent = max*Mathf.Sign(activeExtent);
                    multiplier *= -1;
                }
                activeExtent += (Random.Range(deltaMinPerIteration, deltaMaxPerIteration) * multiplier);
                MoveForward(ProgressAmountByIterations(iterations));
                SpawnProp(PropType.Mountain, PointByExtent(.4f * currentSide, transform.position, activeExtent));
                currentSide *= -1;
            }
        }
	
        // Update is called once per frame
        void Update () {
        }

        void SpawnProp(PropType type, Vector3 position = default(Vector3))
        {
            int randomPool =Random.Range(0, PropPools[type].Count);
            PlanetaryProp newProp = PropPools[type][randomPool].Get();
            
            newProp.PlanetParent = TargetPlanet;
            if (position == default(Vector3))
            {
                newProp.transform.position = RandomPointOnPlanet(transform.position);
            }
            else
            {
                newProp.transform.position = position;
            }
            newProp.AttachmentType = PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().AttachmentType;
            newProp.DistanceOffset = PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().DistanceOffset;
            newProp.WithChildren= PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().WithChildren;
//            newProp.TreeDecorator = PropPools[type][randomPool].DefaultPrimitive.GetComponent<PlanetaryProp>().TreeDecorator;
            newProp.OnSpawn();
        }

        

        Vector3 RandomPointOnPlanet(Vector3 Midpoint, float maxExtent = 1)
        {
            Vector3 point1 = TargetPlanet.transform.position + new Vector3(-TargetPlanet.transform.localScale.x / 2, 0, 0);
            Vector3 point2 = TargetPlanet.transform.position + new Vector3(TargetPlanet.transform.localScale.x / 2, 0, 0);
            
            Vector3 finalPoint = Vector3.Lerp(Vector3.Lerp(point1, Midpoint, Random.Range(0f, maxExtent)), Vector3.Lerp(Midpoint, point2, Random.Range(0f, maxExtent)), Random.Range(0f, 1f));
            return finalPoint;
        }


        Vector3 PointByExtent(float extent, Vector3 midpoint, float extentStart)
        {
            Vector3 point1 = TargetPlanet.transform.position + new Vector3(-TargetPlanet.transform.localScale.x / 2, 0, 0);
            Vector3 point2 = TargetPlanet.transform.position + new Vector3(TargetPlanet.transform.localScale.x / 2, 0, 0);
            if (extentStart + extent > 0)
            {
                return Vector3.Lerp(midpoint, point2, extentStart + Mathf.Abs(extent));
            }
            return Vector3.Lerp(midpoint, point1, -extentStart + Mathf.Abs(extent));
        }
    }
}
