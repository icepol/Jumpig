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

        void Start()
        {
            EventManager.AddListener(Events.CAMERA_SHAKE_BIG, OnCameraShakeBig);
            EventManager.AddListener(Events.CAMERA_SHAKE_SMALL, OnCameraShakeSmall);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.CAMERA_SHAKE_BIG, OnCameraShakeBig);
            EventManager.RemoveListener(Events.CAMERA_SHAKE_SMALL, OnCameraShakeSmall);
        }

        void OnCameraShakeBig()
        {
            _animator.SetTrigger("ShakeBig");
        }

        void OnCameraShakeSmall()
        {
            _animator.SetTrigger("ShakeSmall");
        }
    }
}