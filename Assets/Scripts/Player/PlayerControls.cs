using pixelook;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private IPlayerMovement _playerMovement;

    private bool _isEnabled;
    private bool _isPlayerDead;
    
    private void Awake()
    {
        _playerMovement = GetComponent<IPlayerMovement>();
        
        EventManager.AddListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.AddListener(Events.FLOOR_FALL_STARTED, OnFloorFallStarted);
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
        EventManager.AddListener(Events.SINGLE_TAP, OnSingleTap);
        EventManager.AddListener(Events.DOUBLE_TAP, OnDoubleTap);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Start()
    {
        DisableController();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.RemoveListener(Events.FLOOR_FALL_STARTED, OnFloorFallStarted);
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
        EventManager.RemoveListener(Events.SINGLE_TAP, OnSingleTap);
        EventManager.RemoveListener(Events.DOUBLE_TAP, OnDoubleTap);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        _isPlayerDead = true;
        
        DisableController();
    }

    private void OnFloorMoveFinished()
    {
        EnableController();
    }
    
    private void OnPlayerJumpStarted()
    {
        DisableController();
    }

    private void OnFloorFallStarted()
    {
        DisableController();
    }
    
    private void OnInitFloorFinished()
    {
        EnableController();
    }

    private void OnDoubleTap(Vector3 position)
    {
        if (_isEnabled)
            _playerMovement.MoveTo(position);
    }

    private void OnSingleTap(Vector3 position)
    {
        if (_isEnabled)
            _playerMovement.MoveTo(position);
    }
    
    void EnableController()
    {
        if (_isPlayerDead) return;
        
        // TODO: change animation
        
        _isEnabled = true;
    }

    void DisableController()
    {
        // TODO: change animation
        
        _isEnabled = false;
    }
}
