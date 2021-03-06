using System;
using pixelook;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private float movementDuration = 1;

    private Animator _animator;
    
    private float _currentMovementDuration;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private bool _isMoving;
    private bool _isPlayerDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Update()
    {
        MoveStep();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        _isPlayerDead = true;
    }

    public void MoveStep()
    {
        if (!_isMoving || _isPlayerDead) return;
        
        _currentMovementDuration += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(_oldPosition, _newPosition, _currentMovementDuration / movementDuration);

        if (!(_currentMovementDuration >= movementDuration)) return;
        
        _isMoving = false;
        EventManager.TriggerEvent(Events.PLAYER_JUMP_FINISHED);
    }

    public void MoveTo(Vector3 position)
    {
        EventManager.TriggerEvent(Events.PLAYER_JUMP_STARTED);

        _oldPosition = transform.localPosition;
        _newPosition = _oldPosition + position;
        
        _currentMovementDuration = 0;
        _isMoving = true;
        
        _animator.SetTrigger("Jump");
    }
}
