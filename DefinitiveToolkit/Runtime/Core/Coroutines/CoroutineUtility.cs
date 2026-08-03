using System;
using System.Collections;
using UnityEngine;
using DTK.Core.Coroutines;

namespace DTK.Core.Coroutines
{
    public class CoroutineUtility : MonoBehaviour
    {
        
        #region Delay
        public static CoroutineHandle Delay(float seconds, Action onComplete, bool realTime = false,MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(DelayRoutine(seconds, onComplete, realTime), owner);
        }

        private static IEnumerator DelayRoutine(float seconds, Action onComplete, bool realTime)
        {
            object wait = realTime ? new WaitForSecondsRealtime(seconds) : new WaitForSeconds(seconds);
            yield return wait;
            onComplete?.Invoke();
        }
        #endregion
        
        #region Next Frame
        public static CoroutineHandle NextFrame(Action onComplete, MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(NextFrameRoutine(onComplete), owner);
        }

        private static IEnumerator NextFrameRoutine(Action onComplete)
        {
            yield return null;
            onComplete?.Invoke();
        }
        #endregion
        
        #region Wait Until
        public static CoroutineHandle WaitUntil(Func<bool> condition, Action onComplete, MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(WaitUntilRoutine(condition, onComplete), owner);
        }

        private static IEnumerator WaitUntilRoutine(Func<bool> condition, Action onComplete)
        {
            yield return new UnityEngine.WaitUntil(condition);
            onComplete?.Invoke();
        }
        #endregion
        
        #region Wait While
        public static CoroutineHandle WaitWhile(Func<bool> condition, Action onComplete, MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(WaitWhileRoutine(condition, onComplete), owner);
        }

        private static IEnumerator WaitWhileRoutine(Func<bool> condition, Action onComplete)
        {
            yield return new UnityEngine.WaitWhile(condition);
            onComplete?.Invoke();
        }
        #endregion
        
        #region Repeat
        public static CoroutineHandle Repeat(float interval, Action onTick, bool tickFromZero = false, bool realTime = false, MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(RepeatRoutine(interval, onTick, realTime, tickFromZero), owner);
        }

        private static IEnumerator RepeatRoutine(float interval, Action onTick, bool realTime, bool tickFromZero)
        {
            object wait = realTime ? new WaitForSecondsRealtime(interval) : new WaitForSeconds(interval);

            if (tickFromZero)
            {
                onTick?.Invoke();
            }

            while (true)
            {
                yield return wait;
                onTick?.Invoke();
            }
        }
        #endregion
        
        #region RepeatFor
        public static CoroutineHandle RepeatFor(float interval, int loopCount, Action onTick, Action onComplete = null, bool tickFromZero = false, bool realTime = false, MonoBehaviour owner = null)
        {
            return CoroutineRunner.StartRoutine(RepeatForRoutine(interval, loopCount, onTick, onComplete, tickFromZero, realTime, owner));
        }

        private static IEnumerator RepeatForRoutine(float interval, int count, Action onTick, Action onComplete, bool tickFromZero, bool realTime)
        {
            object wait = realTime ? new WaitForSecondsRealtime(interval) : new WaitForSeconds(interval);
            int remaining = count;

            if (tickFromZero)
            {
                onTick?.Invoke();
                remaining--;
            }

            for (int i = 0; i < remaining; i++)
            {
                yield return wait;
                onTick?.Invoke();
            }

            onComplete?.Invoke();
        }
        #endregion
        
    }
}


