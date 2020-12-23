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
            EventManager.AddListener(Events.CAMERA_SHAKE_BIG, OnCamereShakeBig);
            EventManager.AddListener(Events.CAMERA_SHAKE_SMALL, OnCameraShakeSmall);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.CAMERA_SHAKE_BIG, OnCamereShakeBig);
            EventManager.RemoveListener(Events.CAMERA_SHAKE_SMALL, OnCameraShakeSmall);
        }

        void OnCamereShakeBig()
        {
            _animator.SetTrigger("ShakeBig");
        }

        void OnCameraShakeSmall()
        {
            _animator.SetTrigger("ShakeSmall");
        }
    }
}