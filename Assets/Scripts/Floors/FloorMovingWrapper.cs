using pixelook;
using UnityEngine;

public class FloorMovingWrapper : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.01f;
    [SerializeField] private float maxSpeed = 1f;

    private bool _isGameRunning;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnDisable()
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
    
    private void OnPlayerMoveFinished(Vector3 position)
    {
        _targetPosition = transform.position + Vector3.back * position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameRunning)
            MoveFloor();
    }

    private void MoveFloor()
    {
        float speed = (minSpeed + (transform.position.z - _targetPosition.z)) * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _targetPosition, 
            Mathf.Clamp(speed, minSpeed, maxSpeed));
    }
}
