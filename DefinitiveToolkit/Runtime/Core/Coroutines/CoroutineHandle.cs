using UnityEngine;

public class CoroutineHandle
{
    public Coroutine Coroutine { get; private set; }

    public enum CoroutineStates
    {
        Started,
        Finished,
        Cancelled,
    }

    public CoroutineStates CurrentState { get; private set; }
    public bool IsDone => CurrentState is CoroutineStates.Finished or CoroutineStates.Cancelled;

    public CoroutineHandle()
    {
        CurrentState = CoroutineStates.Started;
    }

    internal void SetCoroutine(Coroutine coroutine)
    {
        Coroutine = coroutine;
    }

    public void MarkFinished()
    {
        if (CurrentState == CoroutineStates.Cancelled) return;
        CurrentState = CoroutineStates.Finished;
    }

    public void MarkCancelled() => CurrentState = CoroutineStates.Cancelled;
}