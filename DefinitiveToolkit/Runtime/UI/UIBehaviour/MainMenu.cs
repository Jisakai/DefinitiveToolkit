using DTK.Core.Services;
using DTK.Core.SceneManagement;
using UnityEngine;

namespace DTK.UI
{
    public class MainMenu : UIPanelBehaviour
    {
        [SerializeField] private SceneRef gameplayScene;

        public void OnPlayClicked()
        {
            ServiceRegistry.Require<SceneService>().LoadSingle(gameplayScene);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}