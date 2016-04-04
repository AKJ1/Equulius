using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Powerup : MonoBehaviour
    {
        public PowerupType Type;
    }

    public enum PowerupType
    {
        Speed,Score,Shield
    }
    
}