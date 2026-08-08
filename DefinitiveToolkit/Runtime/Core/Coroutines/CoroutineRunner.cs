using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTK.Core.Coroutines
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        private readonly List<CoroutineHandle> _activeCoroutines = new();

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    Initialize();
                }
                return _instance;
            }
        }

        private static void Initialize()
        {
            GameObject runner = new GameObject("DTK Coroutine Runner");
            DontDestroyOnLoad(runner);
            _instance = runner.AddComponent<CoroutineRunner>();
        }

        /// <summary>
        /// Starts a coroutine that survives scene loads and, if bound to an owner, 
        /// cancels itself cleanly when that owner is destroyed.
        /// </summary>
        /// <param name="routine">The coroutine logic to run.</param>
        /// <param name="owner">Only assign if the routine references the owner GameObject. If null, defaults to the global manager and runs until completion.</param>
        /// <returns>A handle to check the routine's state or stop it manually.</returns>
        public static CoroutineHandle StartRoutine(IEnumerator routine, MonoBehaviour owner = null)
        {
            
            return Instance._Start(routine, owner ?? Instance);
        }

        private CoroutineHandle _Start(IEnumerator routine, MonoBehaviour owner)
        {
            CoroutineHandle handle = new CoroutineHandle();
            _activeCoroutines.Add(handle);
            handle.SetCoroutine(StartCoroutine(TrackRoutine(routine, handle, owner)));
            
            return handle;
        }

        private IEnumerator TrackRoutine(IEnumerator routine, CoroutineHandle handle, MonoBehaviour owner)
        {
            while (true)
            {
                if (!owner)
                {
                    handle.MarkCancelled();
                    _activeCoroutines.Remove(handle);
                    yield break;
                }

                bool hasMore = routine.MoveNext();

                if (!hasMore)
                {
                    handle.MarkFinished();
                    _activeCoroutines.Remove(handle);
                    yield break;
                }

                yield return routine.Current;
            }
        }

        /// <summary>Stops a specific coroutine and marks it cancelled.</summary>
        public static void StopRoutine(CoroutineHandle handle)
        {
            Instance._Stop(handle);
        }

        private void _Stop(CoroutineHandle handle)
        {
            if (handle == null || !_activeCoroutines.Contains(handle))
                return;

            StopCoroutine(handle.Coroutine);
            handle.MarkCancelled();

            _activeCoroutines.Remove(handle);
        }

        /// <summary>Stops every coroutine and marks them all cancelled.</summary>
        public static void StopAllRoutines() 
        {
            Instance._StopAll();
        }

        private void _StopAll()
        {
            foreach (CoroutineHandle handle in _activeCoroutines)
            {
                StopCoroutine(handle.Coroutine);
                handle.MarkCancelled();
            }
            _activeCoroutines.Clear();
        }
    }
    
}