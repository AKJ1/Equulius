using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util
{
    /// <summary>
    /// An implementation of the pool functionality using FIFO [First in first out]
    /// Useful for reusing Panels
    /// </summary>
    public class PooledQueue<T> : PooledCollection<T> where T: MonoBehaviour
    {

        protected int Capacity;
        protected int PrewarmCount;

        protected GameObject DefaultObject;
        

        public PooledQueue(GameObject defaultObject = null, int capacity = 64, int prewarmCapacity = 16, bool preWarm = false)
        {
            PrewarmCount = prewarmCapacity;
            DefaultObject = defaultObject;
            Capacity = capacity;

            if (preWarm && defaultObject == null)
            {
                Prewarm();
            }
            else if (preWarm)
            {
                PrewarmWith(defaultObject);
            }
        }

        protected Queue<T> InactiveItems;

        protected Queue<T> ActiveItems;

        /// <summary>
        /// Gets the item at the queue's front
        /// </summary>
        /// <returns></returns>
        public override T Get()
        {
            T item = InactiveItems.Dequeue();
            ActiveItems.Enqueue(item);
            InvokeGetObject(item);
            return item;
        }

        /// <summary>
        /// Adds a new item to the queue
        /// </summary>
        /// <param name="item"> the item you wish to add to the collection </param>
        public override void Add(T item)
        {
            InvokeAddObject(item);
            ActiveItems.Enqueue(item);            
        }

        /// <summary>
        /// Deactivates the last item.
        /// </summary>
        public virtual void Deactivate()
        {
            T item = ActiveItems.Dequeue();
            InvokeReturnObject(item);
            InactiveItems.Enqueue(item);            
        }

        /// <summary>
        /// Reverses the entire queue, both active and inactive items.
        /// Useful when creating Self-contained reusable interface elements.
        /// Particullarly ones used in scrollviews.
        /// </summary> 
        public void Reverse()
        {
            Queue<T> reverseArray = new Queue<T>(ActiveItems.Count);
            Queue<T> reverseInactiveArray = new Queue<T>(InactiveItems.Count);

            T[] activeitems = ActiveItems.ToArray();
            for (int i = ActiveItems.Count; i > 0 ; i--)
            {
                reverseArray.Enqueue(activeitems[i]);
            }

            T[] inactiveitems = InactiveItems.ToArray();
            for (int i = InactiveItems.Count; i > 0; i--)
            {
                reverseInactiveArray.Enqueue(inactiveitems[i]);
            }

            ActiveItems = reverseArray;
            InactiveItems = reverseInactiveArray;
        }

        /// <summary>
        /// Fills the pool with the default object.
        /// </summary>
        void Prewarm()
        {
            for (int i = 0; i < PrewarmCount; i++)
            {
                T item = GeneratePrimitive();
                InactiveItems.Enqueue(item);
            }
        }


        /// <summary>
        /// Fills the pool with a custom object, afterwards reverting to the default object.
        /// </summary>
        /// <param name="o">The custom object the pool will be initialized with.</param>
        public void PrewarmWith(GameObject o)
        {
            GameObject def = DefaultObject;
            DefaultObject = o;
            for (int i = 0; i < PrewarmCount; i++)
            {
                T component = GeneratePrimitive();
                InactiveItems.Enqueue(component);
            }
            DefaultObject = def;
        }
        
    }

    public enum ActionPhase
    {
        OnGet,
        OnReturn,
        OnAdd
    }
}
