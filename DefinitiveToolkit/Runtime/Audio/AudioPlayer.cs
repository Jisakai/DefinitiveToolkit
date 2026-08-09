using System.Collections.Generic;
using UnityEngine;

namespace DTK.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        private static AudioPlayer _instance;

        private AudioSource _sfxSource;
        private readonly Dictionary<string, AudioSource> _loopSources = new Dictionary<string, AudioSource>();

        public static AudioPlayer Instance
        {
            get
            {
                if (_instance == null) Initialize();
                return _instance;
            }
        }

        private static void Initialize()
        {
            GameObject go = new GameObject("DTK Audio Player");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<AudioPlayer>();
            _instance.Setup();
        }

        private void Setup()
        {
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlaySFX(AudioClip clip, float volume, float pitch = 1f)
        {
            if (clip == null) return;

            _sfxSource.pitch = pitch;
            _sfxSource.PlayOneShot(clip, volume);
        }

        /// <summary>Gets or creates a dedicated looping AudioSource for a channel (e.g. "Music", "Ambience").</summary>
        public AudioSource GetLoopSource(string channel)
        {
            if (!_loopSources.TryGetValue(channel, out AudioSource source))
            {
                source = gameObject.AddComponent<AudioSource>();
                source.loop = true;
                _loopSources[channel] = source;
            }
            return source;
        }
    }
}