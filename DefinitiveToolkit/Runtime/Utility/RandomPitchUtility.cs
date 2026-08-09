using UnityEngine;

namespace DTK.Utility.Random
{
    public static class RandomPitchUtility
    {
        /// <summary>Returns a pitch value randomized around 1.0 by +/- variance (e.g. 0.1 = 0.9 to 1.1).</summary>
        public static float Randomize(float variance = 0.1f)
        {
            return 1f + UnityEngine.Random.Range(-variance, variance);
        }

        /// <summary>Returns a pitch value randomized within an explicit min/max range.</summary>
        public static float Randomize(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}