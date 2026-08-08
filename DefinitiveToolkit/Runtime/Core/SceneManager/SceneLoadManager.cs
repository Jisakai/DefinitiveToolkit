using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DTK.Core.Coroutines;

namespace DTK.Core.SceneManagement
{
    public class SceneLoadManager
    {
        #region State
        private readonly HashSet<string> _loadedAdditive = new HashSet<string>();
        private bool _isTransitioning;
        #endregion

        #region Events
        public event Action<float> OnLoadProgress;
        public event Action OnTransitionStarted;
        public event Action OnTransitionCompleted;
        #endregion

        #region Single Scene
        public void LoadSingle(SceneRef scene, ISceneTransition transition = null, Action onComplete = null)
        {
            if (_isTransitioning)
            {
                Debug.LogWarning("[SceneLoadManager] Load requested while already transitioning; ignoring.");
                return;
            }

            CoroutineRunner.StartRoutine(LoadSingleRoutine(scene, transition, onComplete));
        }

        private IEnumerator LoadSingleRoutine(SceneRef scene, ISceneTransition transition, Action onComplete)
        {
            _isTransitioning = true;
            OnTransitionStarted?.Invoke();

            if (transition != null)
                yield return transition.PlayOut();

            AsyncOperation op = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Single);

            while (!op.isDone)
            {
                OnLoadProgress?.Invoke(op.progress);
                yield return null;
            }

            _loadedAdditive.Clear(); // single load wipes any tracked additive scenes

            if (transition != null)
                yield return transition.PlayIn();

            _isTransitioning = false;
            OnTransitionCompleted?.Invoke();
            onComplete?.Invoke();
        }
        #endregion

        #region Additive Scenes
        public void LoadAdditive(SceneRef scene, Action onComplete = null)
        {
            if (_loadedAdditive.Contains(scene.SceneName))
            {
                Debug.LogWarning($"[SceneLoadManager] '{scene.SceneName}' is already loaded additively.");
                onComplete?.Invoke();
                return;
            }

            CoroutineRunner.StartRoutine(LoadAdditiveRoutine(scene, onComplete));
        }

        private IEnumerator LoadAdditiveRoutine(SceneRef scene, Action onComplete)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);

            while (!op.isDone)
            {
                OnLoadProgress?.Invoke(op.progress);
                yield return null;
            }

            _loadedAdditive.Add(scene.SceneName);
            onComplete?.Invoke();
        }

        public void UnloadAdditive(SceneRef scene, Action onComplete = null)
        {
            if (!_loadedAdditive.Contains(scene.SceneName))
            {
                Debug.LogWarning($"[SceneLoadManager] '{scene.SceneName}' isn't loaded additively; cannot unload.");
                onComplete?.Invoke();
                return;
            }

            CoroutineRunner.StartRoutine(UnloadAdditiveRoutine(scene, onComplete));
        }

        private IEnumerator UnloadAdditiveRoutine(SceneRef scene, Action onComplete)
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync(scene.SceneName);

            while (!op.isDone)
                yield return null;

            _loadedAdditive.Remove(scene.SceneName);
            onComplete?.Invoke();
        }
        #endregion

        #region Scene Sets
        public void LoadSet(IEnumerable<SceneRef> scenes, ISceneTransition transition = null, Action onComplete = null)
        {
            CoroutineRunner.StartRoutine(LoadSetRoutine(scenes, transition, onComplete));
        }

        private IEnumerator LoadSetRoutine(IEnumerable<SceneRef> scenes, ISceneTransition transition, Action onComplete)
        {
            _isTransitioning = true;
            OnTransitionStarted?.Invoke();

            if (transition != null)
                yield return transition.PlayOut();

            foreach (SceneRef scene in scenes)
            {
                if (_loadedAdditive.Contains(scene.SceneName)) continue;

                AsyncOperation op = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
                while (!op.isDone)
                {
                    OnLoadProgress?.Invoke(op.progress);
                    yield return null;
                }
                _loadedAdditive.Add(scene.SceneName);
            }

            if (transition != null)
                yield return transition.PlayIn();

            _isTransitioning = false;
            OnTransitionCompleted?.Invoke();
            onComplete?.Invoke();
        }
        #endregion
    }
}