using System;
using Assets.Scripts.Util.Contracts;
using UnityEngine;

namespace Assets.Scripts.Util
{
    /// <summary>
    /// The base class for managed pooled collections.
    /// Extend if you wish to create your own pooled data type.
    /// </summary>
    public abstract class PooledCollection<T> : IPooledCollection<T> where T : MonoBehaviour
    {
        public event Action<T> OnGetObject;
        public event Action<T> OnAddObject;
        public event Action<T> OnReturnObject;

        public GameObject DefaultPrimitive;

        protected void InvokeGetObject(T item)
        {
            if (OnGetObject != null)
            {
                OnGetObject(item);
            }
        }

        protected void InvokeReturnObject(T item)
        {
            if (OnReturnObject != null)
            {
                OnReturnObject(item);
            }
        }
        protected void InvokeAddObject(T item)
        {
            if (OnAddObject != null)
            {
                OnAddObject(item);
            }
        }

        public abstract T Get();

        public abstract void Add(T item);

        /// <summary>
        /// Generates a primitive object. If no primitive object is set, generates a blank.
        /// </summary>
        /// <returns>An object of type T that is of type T.</returns>
        protected T GeneratePrimitive()
        {
            GameObject go = DefaultPrimitive == null ? MasterPool.Get() : GameObject.Instantiate(DefaultPrimitive);

            T component = go.AddComponent<T>();

            go.SetActive(false);

            return component;
        }
    }
}
