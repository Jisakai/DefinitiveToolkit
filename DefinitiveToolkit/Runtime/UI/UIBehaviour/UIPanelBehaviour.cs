using UnityEngine;
using DTK.Core.Services;

namespace DTK.UI
{
    public abstract class UIPanelBehaviour : MonoBehaviour, IUIPanel
    {
        [SerializeField] private string panelId;

        protected virtual void OnEnable() => ServiceRegistry.Require<UIService>().Register(panelId, this);
        protected virtual void OnDisable() => ServiceRegistry.Require<UIService>().Unregister(panelId);

        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}