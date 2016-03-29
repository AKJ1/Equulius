namespace Assets.Scripts.Util
{
    using UnityEngine;
    using System.Collections.Generic;

    public class Pool<T> : PooledCollection<T> where T: MonoBehaviour
    {
        /// <summary>
        /// A hashset is used because it is more efficient than a generic list and allows for greater access speed to our collection.
        /// </summary>
        private HashSet<T> activeObjects;

        /// <summary>
        /// A stack is used because it is assumed that recycled gameobjects will be reset within the developer's code.
        /// </summary>
        private Stack<T> inactiveObjects;
        
        private int capacity;

        private int preWarmCapacity = 16;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity">The pool's caximum capacity. Defaults to 64. </param>
        /// <param name="preWarm">Whether the objects in the pool are pre-instantiated. Enable this if you want to have a controlled lag spike.</param>
        /// <param name="defaultAction">An Action to do on an object before it's handed off to the caller. </param>
        /// <param name="defaultPrimitive"> A Default primitive to use as a template. When this is not assigned it will return a blank game object with a default Component. </param>
        public Pool(GameObject defaultPrimitive = null, int capacity = 64, bool preWarm = false)
        {
            activeObjects = new HashSet<T>();

            inactiveObjects = new Stack<T>(capacity);

            this.capacity = capacity;

            DefaultPrimitive = defaultPrimitive;
            

            if (preWarm)
            {
                Prewarm();
            }
        }

        /// <summary>
        /// Adds the non-pooled item of the type T to the pool.
        /// </summary>
        /// <param name="item">The item to Include</param>
        public override void Add(T item)
        {
            InvokeAddObject(item);
            
            activeObjects.Add(item);
        }

        /// <summary>
        /// Deactivates an Object
        /// </summary>
        /// <param name="item">The Object to deactivate</param>
        public void Deactivate(T item)
        {
            InvokeReturnObject(item);

            item.gameObject.SetActive(false);

            activeObjects.Remove(item);
            
            inactiveObjects.Push(item);
        }

        /// <summary>
        /// Returns an object ready for use.
        /// </summary>
        /// <returns>Ready-to-use Object</returns>
        public override T Get()
        {
//            Debug.Log(inactiveObjects.Count);
//            Debug.Log(activeObjects.Count);
            T comp = inactiveObjects.Count <= 0 ? GeneratePrimitive() : inactiveObjects.Pop();
            comp.gameObject.SetActive(true);
            activeObjects.Add(comp);
            InvokeGetObject(comp);
            return comp;
        }


        /// <summary>
        /// Used in case where spawning objects during gameplay is not good. 
        /// Initializes objects the moment it's created, leading to an initial 
        /// performance hit followed by smooth performance afterwards.
        /// </summary>
        private void Prewarm()
        {
            for (int i = 0; i < (capacity > MasterPool.MaxPooledCapacity ? preWarmCapacity : capacity ); i++)
            {
                inactiveObjects.Push(GeneratePrimitive());
            }
        }


    }
}