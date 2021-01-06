using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerJump;
        [SerializeField] private AudioClip playerFall;
        [SerializeField] private AudioClip playerObstacleContact;
        [SerializeField] private AudioClip pickupCoin;
        [SerializeField] private AudioClip pickupFood;
        [SerializeField] private AudioClip platformMove;
        [SerializeField] private AudioClip levelFinished;

        private void Start()
        {
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
                AudioSource.PlayClipAtPoint(playerJump, targetTransform.position);
        }
        
        private void OnPlayerFallen()
        {
            if (playerFall && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerFall, targetTransform.position);
        }
        
        private void OnPlayerCollidedObstacle()
        {
            if (playerObstacleContact && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerObstacleContact, targetTransform.position);
        }
        
        private void OnCoinCollected()
        {
            if (pickupCoin && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(pickupCoin, targetTransform.position);
        }
        
        private void OnFoodCollected()
        {
            if (pickupFood && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(pickupFood, targetTransform.position);
        }
        
        private void OnFloorMoveStarted()
        {
            if (platformMove && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(platformMove, targetTransform.position);
        }
    }
}