using UnityEngine;
using DTK.Core.Services;

namespace DTK.Core.Save
{
    public abstract class SaveableBehaviour : MonoBehaviour, ISaveable
    {
        public abstract string SaveId { get; }
        public abstract string CaptureState();
        public abstract void RestoreState(string json);

        protected virtual void OnEnable() => ServiceRegistry.Require<SaveService>().Register(this);
        protected virtual void OnDisable() => ServiceRegistry.Require<SaveService>().Unregister(this);
    }
}