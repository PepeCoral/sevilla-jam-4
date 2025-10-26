using HandyScripts.Timer;
using UnityEngine;

namespace HandyScripts
{
    public class DestroyInTime : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private AutomaticTimer _timer;

        private void Start()
        {
            _timer = gameObject.AddComponent<AutomaticTimer>();
            _timer.Setup(_lifeTime);
            _timer.StartTimer();
        }

        private void Update()
        {
            if (_timer.HasFinished) Destroy(gameObject);
        }
    }
}