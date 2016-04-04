using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CoinSpawnLocation : MonoBehaviour
    {
        public PowerupType PowerupType;

        public void Start()
        {

            if (Random.Range(0,100f) < StaticData.percentToSpawnCoin)
            {
                GameObject go = (GameObject) GameObject.Instantiate(StaticData.PowerupsByType[PowerupType].gameObject, transform.position,
                    Quaternion.identity);
                go.transform.parent = transform.parent;
                go.transform.position = transform.position;
                go.AddComponent<SpinnyComponent>();
            }
            Destroy(gameObject);
        }

    }
}