using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioClip enemyDie;
        [SerializeField] private AudioClip fire;
        [SerializeField] private AudioClip playerDie;

        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        }
        
        public void OnPlayerDied()
        {
            if (playerDie && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerDie, _cameraTransform.position);
        }
    }
}