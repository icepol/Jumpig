using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem onPlayerCollidedObstacleParticle;
    
    private Rigidbody _rigidbody;
    
    private int _floorLayerMask;
    private bool _isGameRunning;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _floorLayerMask = 1 << LayerMask.NameToLayer("Floor");
    }

    void Start()
    {
        EventManager.AddListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
        EventManager.AddListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
        EventManager.RemoveListener(Events.PLAYER_COLLIDED_OBSTACLE, OnPlayerCollidedObstacle);
    }

    private void OnPlayerCollidedObstacle()
    {
        if (onPlayerCollidedObstacleParticle != null)
            Instantiate(onPlayerCollidedObstacleParticle, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    private void OnPlayerJumpStarted()
    {
        if (!_isGameRunning)
        {
            _isGameRunning = true;
        }
        
        transform.parent = null;
    }

    private void OnPlayerJumpFinished()
    {
        PlaceOnFloor();
    }

    private void OnInitFloorFinished()
    {
        PlaceOnFloor();
    }

    void PlaceOnFloor()
    {
        if (Physics.Linecast(
            transform.position,
            transform.position + Vector3.down,
            out var hit,
            _floorLayerMask))
        {
            transform.parent = hit.transform;

            if (!_isGameRunning) return;
            
            GameState.Score += 5;
            GameState.Distance++;

            EventManager.TriggerEvent(Events.PLAYER_MOVEMENT_FINISHED);
        }
        else
        {
            NoFloor();
        }
    }

    void NoFloor()
    {
        _rigidbody.isKinematic = false;

        EventManager.TriggerEvent(Events.PLAYER_DIED);
    }
}