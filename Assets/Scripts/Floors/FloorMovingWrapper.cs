using pixelook;
using UnityEngine;

public class FloorMovingWrapper : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.01f;
    [SerializeField] private float maxSpeed = 1f;
    
    private Player _player;
    private Vector3 _playerOriginPosition;

    private bool _isGameRunning;
    private Vector3 _targetPosition;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
        
        _player = FindObjectOfType<Player>();
        _playerOriginPosition = _player.transform.position;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnGameStarted()
    {
        _isGameRunning = true;
    }
    
    private void OnPlayerDied()
    {
        _isGameRunning = false;
    }
    
    private void OnPlayerMoveFinished()
    {
        _targetPosition = transform.position + Vector3.back * (_player.transform.position.z - _playerOriginPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameRunning)
            MoveFloor();
    }

    private void MoveFloor()
    {
        float speed = minSpeed + (transform.position.z - _targetPosition.z) * 0.015f;
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _targetPosition, 
            Mathf.Clamp(speed, minSpeed, maxSpeed));
    }
}
