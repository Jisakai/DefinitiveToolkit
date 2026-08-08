using System;
using UnityEngine;

namespace DTK.Core.SceneManagement
{
    [Serializable]
    public struct SceneRef
    {
        [SerializeField] private string sceneName;

        public string SceneName => sceneName;

        public SceneRef(string sceneName)
        {
            this.sceneName = sceneName;
        }

        public static implicit operator string(SceneRef sceneRef) => sceneRef.sceneName;
    }
}