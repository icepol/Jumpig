using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioClip playerJump;
        [SerializeField] private AudioClip playerFall;
        [SerializeField] private AudioClip playerObstacleContact;
        [SerializeField] private AudioClip pickupCoin;
        [SerializeField] private AudioClip pickupFood;
        [SerializeField] private AudioClip platformMove;
        [SerializeField] private AudioClip levelFinished;

        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
            EventManager.AddListener(Events.PLAYER_FALLEN, OnPlayerFallen);
            EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.AddListener(Events.COIN_COLLECTED, OnCoinCollected);
            EventManager.AddListener(Events.FOOD_COLLECTED, OnFoodCollected);
            EventManager.AddListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
            EventManager.RemoveListener(Events.PLAYER_FALLEN, OnPlayerFallen);
            EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
            EventManager.RemoveListener(Events.COIN_COLLECTED, OnCoinCollected);
            EventManager.RemoveListener(Events.FOOD_COLLECTED, OnFoodCollected);
            EventManager.RemoveListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
        }

        private void OnPlayerJumpStarted()
        {
            if (playerJump && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerJump, _cameraTransform.position);
        }
        
        private void OnPlayerFallen()
        {
            if (playerFall && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerFall, _cameraTransform.position);
        }
        
        private void OnPlayerCollidedObstacle()
        {
            if (playerObstacleContact && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerObstacleContact, _cameraTransform.position);
        }
        
        private void OnCoinCollected()
        {
            if (pickupCoin && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(pickupCoin, _cameraTransform.position);
        }
        
        private void OnFoodCollected()
        {
            if (pickupFood && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(pickupFood, _cameraTransform.position);
        }
        
        private void OnFloorMoveStarted()
        {
            if (platformMove && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(platformMove, _cameraTransform.position);
        }
    }
}