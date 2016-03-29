using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Planet : MonoBehaviour
    {
        public SphereCollider AtmosphereCollider;

        public SphereCollider GroundCollider;

        public float GroundRadius { get { return GroundCollider.radius; } }

        public float AtmosphereRadius { get { return GroundRadius * 2f; } }


    }
}