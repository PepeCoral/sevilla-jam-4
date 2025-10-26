using System;
using UnityEngine;

namespace HandyScripts.Timer
{
    public class AutomaticTimer : MonoBehaviour, ITimer
    {
        private ManualTimer _manualTimer;

        public float ElapsedTime => _manualTimer != null ? _manualTimer.ElapsedTime : 0;
        public float GoalTime => _manualTimer.GoalTime;

        private void Update()
        {
            if (_manualTimer == null) return;
            _manualTimer.Update(Time.deltaTime);
        }

        public bool HasFinished => _manualTimer != null ? _manualTimer.HasFinished : false;


        public void StartTimer()
        {
            _manualTimer.StartTimer();
        }

        public void StopTimer()
        {
            _manualTimer.StopTimer();
        }

        public void ResetTimer()
        {
            _manualTimer.ResetTimer();
        }

        public void Setup(float goalTime, Action callback = null)
        {
            if (_manualTimer != null)
            {
                Debug.LogWarning("Timer was already setup");
                return;
            }

            _manualTimer = new ManualTimer(goalTime, callback);
        }
    }
}