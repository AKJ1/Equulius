namespace Assets.Scripts.Util
{
    public static class MathExtensions
    {
        public static float Clamp01Sloped(float value, float innerMax, float outerMax, float innerMin = 0, float outerMin =0)
        {
            if (value <= outerMin)
            {
                return 0;
            }
            if (value <= outerMax)
            {
                return 1;
            }
            return 0;
        }
    }
}