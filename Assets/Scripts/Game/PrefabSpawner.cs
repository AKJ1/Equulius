using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PrefabSpawner : MonoBehaviour
    {
        public GameObject Prefab;

        public void Awake()
        {
            GameObject go = (GameObject) GameObject.Instantiate(Prefab, transform.position, transform.rotation);
            go.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
}