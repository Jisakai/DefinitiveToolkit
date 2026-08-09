using DTK.Core.Services;

namespace DTK.Core.TimeSystem
{
    public static class TimeUtility
    {
        #region Pause
        public static void Pause(string key = "Pause")
        {
            ServiceRegistry.Require<TimeService>().SetTimeScale(key, 0f);
        }

        public static void Resume(string key = "Pause")
        {
            ServiceRegistry.Require<TimeService>().ClearTimeScale(key);
        }
        #endregion

        #region Slow Motion
        public static void SlowMo(string key, float scale)
        {
            ServiceRegistry.Require<TimeService>().SetTimeScale(key, scale);
        }

        public static void ClearSlowMo(string key)
        {
            ServiceRegistry.Require<TimeService>().ClearTimeScale(key);
        }
        #endregion

        #region Delta Time
        public static float DeltaTime => ServiceRegistry.Require<TimeService>().DeltaTime;
        public static float UnscaledDeltaTime => ServiceRegistry.Require<TimeService>().UnscaledDeltaTime;
        #endregion
    }
}
