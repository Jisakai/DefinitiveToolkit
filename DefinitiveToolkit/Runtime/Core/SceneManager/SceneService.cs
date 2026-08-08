using System;
using System.Collections.Generic;
using DTK.Core.Services;

namespace DTK.Core.SceneManagement
{
    public class SceneService : IService
    {
        private readonly SceneLoadManager _manager;

        public SceneService(SceneLoadManager manager)
        {
            _manager = manager;
        }

        public event Action<float> OnLoadProgress
        {
            add => _manager.OnLoadProgress += value;
            remove => _manager.OnLoadProgress -= value;
        }

        public void LoadSingle(SceneRef scene, ISceneTransition transition = null, Action onComplete = null)
            => _manager.LoadSingle(scene, transition, onComplete);

        public void LoadAdditive(SceneRef scene, Action onComplete = null)
            => _manager.LoadAdditive(scene, onComplete);

        public void UnloadAdditive(SceneRef scene, Action onComplete = null)
            => _manager.UnloadAdditive(scene, onComplete);

        public void LoadSet(IEnumerable<SceneRef> scenes, ISceneTransition transition = null, Action onComplete = null)
            => _manager.LoadSet(scenes, transition, onComplete);
    }
}