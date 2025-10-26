using System;

namespace HandyScripts.Timer
{
    public class ManualTimer : ITimer
    {
        private readonly Action _callback;
        private bool _paused = true;

        public ManualTimer(float goalTime, Action callback = null)
        {
            SetGoalTime(goalTime);
            _callback = callback;
        }

        public float ElapsedTime { get; private set; }

        public float GoalTime { get; private set; }

        public bool HasFinished => ElapsedTime >= GoalTime;

        public void StartTimer()
        {
            _paused = false;
        }

        public void StopTimer()
        {
            _paused = true;
        }

        public void ResetTimer()
        {
            ElapsedTime = 0;
        }

        public void SetGoalTime(float goalTime)
        {
            if (goalTime <= 0) throw new ArgumentException("The goal time must be greater than zero");
            GoalTime = goalTime;
        }

        public void Update(float deltaTime)
        {
            if (deltaTime <= 0) throw new ArgumentException("The delta time must be greater than zero");
            if (_paused) return;
            if (HasFinished) return;
            ElapsedTime += deltaTime;
        }
    }
}