using System.Collections;
using UnityEngine;

namespace pixelook
{
    public class DelayAnimation : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.enabled = false;

            StartCoroutine(WaitAndAnimate());
        }

        IEnumerator WaitAndAnimate()
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.5f));

            _animator.enabled = true;
        }
    }
}