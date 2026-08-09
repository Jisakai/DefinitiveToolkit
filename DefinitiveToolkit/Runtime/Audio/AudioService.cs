using UnityEngine;
using DTK.Core.Services;

namespace DTK.Audio
{
    public class AudioService : IService
    {
        private readonly AudioManager _manager;

        public AudioService(AudioManager manager)
        {
            _manager = manager;
        }

        public void SetVolume(string channel, float volume) => _manager.SetVolume(channel, volume);
        public float GetVolume(string channel) => _manager.GetVolume(channel);

        public void PlaySFX(AudioClip clip, string channel = "SFX") => _manager.PlaySFX(clip, channel);

        public void PlayLoop(string channel, AudioClip clip, float fadeDuration = 0.5f) => _manager.PlayLoop(channel, clip, fadeDuration);
        public void StopLoop(string channel, float fadeDuration = 0.5f) => _manager.StopLoop(channel, fadeDuration);
        public void PlaySFX(AudioClip clip, string channel = "SFX", float pitchVariance = 0f)
            => _manager.PlaySFX(clip, channel, pitchVariance);
        
    }
}