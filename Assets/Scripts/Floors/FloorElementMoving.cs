using pixelook;
using UnityEngine;

public class FloorElementMoving : MonoBehaviour
{
    [SerializeField] private float movementDuration = 1f;
    [SerializeField] private Transform[] waypoints;

    private FloorElement _floorElement;
    private int _nextWaypointIndex;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    private bool _isMoving;
    private float _currentMovementTime;

    private void Awake()
    {
        _floorElement = GetComponentInChildren<FloorElement>();
    }

    void Start()
    {
        // put to init position
        _floorElement.transform.position = waypoints[0].position;
        _nextWaypointIndex = 1;
        
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void Update()
    {
        if (!_isMoving) return;

        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        _floorElement.transform.position = Vector3.Lerp(
            _currentPosition, 
            _targetPosition, 
            _currentMovementTime / movementDuration);

        if (_currentMovementTime > movementDuration)
        {
            _isMoving = false;
            _nextWaypointIndex++;

            if (_nextWaypointIndex >= waypoints.Length)
                _nextWaypointIndex = 0;
        }

        _currentMovementTime += Time.deltaTime;
    }

    private void OnPlayerJumpStarted()
    {
        _isMoving = true;
        _currentMovementTime = 0;
        _currentPosition = _floorElement.transform.position;
        _targetPosition = waypoints[_nextWaypointIndex].position;
    }
    
    private void OnPlayerJumpFinished()
    {
        _isMoving = false;
    }
}
