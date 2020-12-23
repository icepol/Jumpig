using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private int _floorLayerMask;
    private bool isGameRunning;

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
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnPlayerJumpStarted()
    {
        if (!isGameRunning)
        {
            isGameRunning = true;
            GameState.Reset();
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

            if (!isGameRunning) return;
            
            GameState.Score += 5;
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