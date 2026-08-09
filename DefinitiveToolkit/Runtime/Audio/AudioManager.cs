using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTK.Core.Coroutines;
using DTK.Utility.Random;

namespace DTK.Audio
{
    public class AudioManager
    {
        private const string MasterChannel = "Master";

        private readonly Dictionary<string, float> _channelVolumes = new Dictionary<string, float>();

        #region Volume
        
        private readonly HashSet<string> _activeLoopChannels = new HashSet<string>();

        public void SetVolume(string channel, float volume)
        {
            _channelVolumes[channel] = Mathf.Clamp01(volume);

            if (channel == MasterChannel)
            {
                foreach (string active in _activeLoopChannels)
                    AudioPlayer.Instance.GetLoopSource(active).volume = EffectiveVolume(active);
            }
            else if (_activeLoopChannels.Contains(channel))
            {
                AudioPlayer.Instance.GetLoopSource(channel).volume = EffectiveVolume(channel);
            }
        }

        public float GetVolume(string channel) => _channelVolumes.GetValueOrDefault(channel, 1f);

        private float EffectiveVolume(string channel)
        {
            if (channel == MasterChannel) return GetVolume(MasterChannel);
            return GetVolume(MasterChannel) * GetVolume(channel);
        }
        #endregion

        #region SFX
        public void PlaySFX(AudioClip clip, string channel = "SFX", float pitchVariance = 0f)
        {
            if (clip == null)
            {
                Debug.LogWarning("[AudioManager] Tried to play a null SFX clip.");
                return;
            }

            float pitch = pitchVariance > 0f ? RandomPitchUtility.Randomize(pitchVariance) : 1f;
            AudioPlayer.Instance.PlaySFX(clip, EffectiveVolume(channel), pitch);
        }
        #endregion

        #region Loops (Music, Ambience, or any custom channel)
        public void PlayLoop(string channel, AudioClip clip, float fadeDuration = 0.5f)
        {
            if (clip == null) { Debug.LogWarning($"[AudioManager] Tried to play a null clip on loop channel '{channel}'."); return; }
            _activeLoopChannels.Add(channel);
            CoroutineUtility.Start(() => CoroutineRunner.StartRoutine(CrossfadeRoutine(channel, clip, fadeDuration)));
        }

        public void StopLoop(string channel, float fadeDuration = 0.5f)
        {
            _activeLoopChannels.Remove(channel);
            CoroutineUtility.Start(() => CoroutineRunner.StartRoutine(CrossfadeRoutine(channel, null, fadeDuration)));
        }
        
        private IEnumerator CrossfadeRoutine(string channel, AudioClip newClip, float duration)
        {
            AudioSource source = AudioPlayer.Instance.GetLoopSource(channel);
            float startVolume = source.volume;
            float targetVolume = EffectiveVolume(channel);

            float t = 0f;
            while (t < duration)
            {
                t += UnityEngine.Time.deltaTime;
                source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
                yield return null;
            }

            source.Stop();
            source.clip = newClip;

            if (newClip == null)
            {
                source.volume = targetVolume;
                yield break;
            }

            source.Play();

            t = 0f;
            while (t < duration)
            {
                t += UnityEngine.Time.deltaTime;
                source.volume = Mathf.Lerp(0f, targetVolume, t / duration);
                yield return null;
            }

            source.volume = targetVolume;
        }
        #endregion
    }
}