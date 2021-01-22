using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem onPlayerCollidedObstacleParticle;
    [SerializeField] private ParticleSystem onPlayerJumpStartedParticle;
    [SerializeField] private ParticleSystem onPlayerJumpFinishedParticle;

    private Rigidbody _rigidbody;
    
    private int _floorLayerMask;
    private int _finishLineLayerMask;
    private bool _isGameRunning;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _floorLayerMask = 1 << LayerMask.NameToLayer("Floor");
        _finishLineLayerMask = 1 << LayerMask.NameToLayer("FinishLine");
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

        if (onPlayerJumpStartedParticle != null)
            Instantiate(onPlayerJumpStartedParticle, transform.position, Quaternion.identity);
    }

    private void OnPlayerJumpFinished()
    {
        PlaceOnFloor();
        CheckFinishLine();
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
            
            if (onPlayerJumpFinishedParticle != null)
                Instantiate(onPlayerJumpFinishedParticle, transform.position - Vector3.down * 0.5f, Quaternion.identity);
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
        EventManager.TriggerEvent(Events.PLAYER_FALLEN);
    }

    void CheckFinishLine()
    {
        if (Physics.Linecast(
            transform.position,
            transform.position + Vector3.up * 10,
            out var hit,
            _finishLineLayerMask))
        {
            EventManager.TriggerEvent(Events.FINISH_LINE_PASSED);
            GameState.Level++;
        }
    }
}