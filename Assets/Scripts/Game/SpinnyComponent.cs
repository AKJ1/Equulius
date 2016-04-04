using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SpinnyComponent : MonoBehaviour
    {
        public void Update()
        {
            transform.Rotate(transform.parent.up,1080 * Time.deltaTime);
        } 
    }
}