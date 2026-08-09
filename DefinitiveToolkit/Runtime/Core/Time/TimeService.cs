using DTK.Core.Services;

namespace DTK.Core.TimeSystem
{
    public class TimeService : IService
    {
        private readonly TimeManager _manager;

        public TimeService(TimeManager manager)
        {
            _manager = manager;
        }

        public float CurrentTimeScale => _manager.CurrentTimeScale;
        public float DeltaTime => _manager.DeltaTime;
        public float UnscaledDeltaTime => _manager.UnscaledDeltaTime;

        public void SetTimeScale(string key, float scale) => _manager.SetTimeScale(key, scale);
        public void ClearTimeScale(string key) => _manager.ClearTimeScale(key);
    }
}
