using System;
using DTK.Core.Services;
using UnityEngine;

namespace DTK.Core.TimeSystem
{
    [CreateAssetMenu(menuName = "DTK/Services/Time Service Installer")]
    public class TimeServiceInstaller : ServiceInstaller
    {
        public override Type ServiceType => typeof(TimeService);

        public override IService CreateService()
        {
            return new TimeService(new TimeManager());
        }
    }
}
