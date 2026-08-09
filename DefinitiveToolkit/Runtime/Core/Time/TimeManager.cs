using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.TimeSystem
{
    public class TimeManager
    {
        private readonly Dictionary<string, float> _modifiers = new Dictionary<string, float>();

        public float CurrentTimeScale => UnityEngine.Time.timeScale;
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;

        public void SetTimeScale(string key, float scale)
        {
            _modifiers[key] = scale;
            Recompute();
        }

        public void ClearTimeScale(string key)
        {
            if (_modifiers.Remove(key))
            {
                Recompute();
            }
            else
            {
                Debug.LogWarning($"[TimeManager] Attempted to clear modifier '{key}', but it wasn't set");
            }
        }

        private void Recompute()
        {
            float combined = 1f;

            foreach (float modifier in _modifiers.Values)
            {
                combined *= modifier;
            }

            UnityEngine.Time.timeScale = Mathf.Max(0f, combined);
        }
    }
}
