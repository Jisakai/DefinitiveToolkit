namespace DTK.Core.Save
{
    public interface ISaveable
    {
        string SaveId { get; }
        string CaptureState();
        void RestoreState(string json);
    }
}