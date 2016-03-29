using System;   
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public static class MasterPool
    {
        public static int MaxPooledCapacity = 32;
        public static Stack<GameObject> InactiveObjects;
        
        public static GameObject Get()
        {
            if (InactiveObjects.Count > 0)
            {
                return InactiveObjects.Pop();
            }
            return new GameObject("PoolObject");
        }

        public static GameObject[] GetMany(int count)
        {
            GameObject[] retarr = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                retarr[i] = InactiveObjects.Pop();
            }
            return retarr;
        }
    }
}
