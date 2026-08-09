using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.Audio
{
    [CreateAssetMenu(menuName = "DTK/Services/Audio Service Installer")]
    public class AudioServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(AudioService);

        public override IService CreateService()
        {
            return new AudioService(new AudioManager());
        }
    }
}