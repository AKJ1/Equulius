namespace Assets.Scripts.Game
{
    using UnityEngine;
    using System.Collections.Generic;

    public class DataInitializer : MonoBehaviour
    {
        public PowerupType[] PowerupTypes;
        public Powerup[] Powerups;

        public void Awake()
        {
            Dictionary<PowerupType, Powerup> powerDict = new Dictionary<PowerupType, Powerup>();

            for (int i = 0; i < PowerupTypes.Length; i++)
            {
                powerDict.Add(PowerupTypes[i], Powerups[i]);
            }
            StaticData.Setup(powerDict);
        }
    }
}