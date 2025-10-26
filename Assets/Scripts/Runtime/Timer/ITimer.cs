namespace HandyScripts.Timer
{
    public interface ITimer
    {
        bool HasFinished { get; }
        void StartTimer();
        void StopTimer();
        void ResetTimer();
    }
}