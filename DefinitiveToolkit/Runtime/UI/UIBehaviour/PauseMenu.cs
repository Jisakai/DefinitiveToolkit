using UnityEngine;
using DTK.Core.Services;
using DTK.Core.TimeSystem;

namespace DTK.UI
{
    public class PauseMenu : UIPanelBehaviour
    {
        public void OnResumeClicked() => ServiceRegistry.Require<UIService>().Close();

        public void OnOptionsClicked() => ServiceRegistry.Require<UIService>().Open("Options");

        public void OnMainMenuClicked()
        {
            TimeUtility.Resume();
            ServiceRegistry.Require<UIService>().CloseAll();
            // scene transition to main menu goes here, via SceneService
        }

        public override void Show()
        {
            base.Show();
            TimeUtility.Pause();
        }

        public override void Hide()
        {
            base.Hide();
            TimeUtility.Resume();
        }
    }
}