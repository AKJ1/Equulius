using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public static class StaticData
    {
        public static bool InsaneMode = false;

        public static float percentToSpawnCoin = 5f;

        public static Dictionary<PowerupType, Powerup> PowerupsByType;


        public static void Setup(Dictionary<PowerupType, Powerup> powerups)
        {
            PowerupsByType = powerups;
        }
    }
}