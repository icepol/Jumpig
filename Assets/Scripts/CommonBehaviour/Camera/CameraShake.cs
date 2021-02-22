using UnityEngine;

namespace pixelook
{
    public class CameraShake : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.AddListener(Events.FLOOR_FALL_STARTED, OnFloorFallStarted);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
            EventManager.RemoveListener(Events.FLOOR_FALL_STARTED, OnFloorFallStarted);
        }

        private void OnPlayerDied()
        {
            _animator.SetTrigger("ShakeBig");
        }

        private void OnFloorFallStarted()
        {
            _animator.SetTrigger("ShakeSmall");
        }
    }
}