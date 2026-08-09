using UnityEngine;
using UnityEngine.UI;
using DTK.Core.Services;
using DTK.Audio;
using DTK.Core.Save;

namespace DTK.UI
{
    public class OptionsMenu : UIPanelBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            AudioService audio = ServiceRegistry.Require<AudioService>();

            masterSlider.value = audio.GetVolume("Master");
            musicSlider.value = audio.GetVolume("Music");
            sfxSlider.value = audio.GetVolume("SFX");

            masterSlider.onValueChanged.AddListener(v => audio.SetVolume("Master", v));
            musicSlider.onValueChanged.AddListener(v => audio.SetVolume("Music", v));
            sfxSlider.onValueChanged.AddListener(v => audio.SetVolume("SFX", v));
        }

        public void OnBackClicked() => ServiceRegistry.Require<UIService>().Close();
    }
}