using pixelook;
using UnityEngine;

public class FloorMovingWrapper : MonoBehaviour
{
    private bool _isGameRunning;
    private bool _isFloorMoving;
    
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    
    private Player _player;
    private Vector3 _playerOriginPosition;

    private float _moveTime = 1f;
    private float _currentMoveTime;

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
        _sourcePosition = transform.position;
        _targetPosition = _sourcePosition + Vector3.back * (_player.transform.position.z - _playerOriginPosition.z); 
        
        _currentMoveTime = 0;

        if (_isFloorMoving) return;
        
        _isFloorMoving = true;
        EventManager.TriggerEvent(Events.FLOOR_MOVE_STARTED);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameRunning && _isFloorMoving)
            MoveFloor();
    }

    private void MoveFloor()
    {
        _currentMoveTime += Time.deltaTime;
        
        transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, _currentMoveTime / _moveTime);

        if (!(_currentMoveTime >= _moveTime)) return;
        
        _isFloorMoving = false;
        EventManager.TriggerEvent(Events.FLOOR_MOVE_FINISHED);
    }
}
