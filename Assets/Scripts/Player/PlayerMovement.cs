using pixelook;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private float movementDuration = 1;

    private float _currentMovementDuration;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private bool _isMoving;
    
    private void Update()
    {
        MoveStep();
    }

    public void MoveStep()
    {
        if (!_isMoving) return;
        
        _currentMovementDuration += Time.deltaTime;
        transform.position = Vector3.Lerp(_oldPosition, _newPosition, _currentMovementDuration / movementDuration);

        if (_currentMovementDuration >= movementDuration)
        {
            _isMoving = false;
            EventManager.TriggerEvent(Events.PLAYER_JUMP_FINISHED);
        }
    }

    public void MoveTo(Vector3 position)
    {
        EventManager.TriggerEvent(Events.PLAYER_JUMP_STARTED);

        _oldPosition = transform.position;
        _newPosition = _oldPosition + position;
        _currentMovementDuration = 0;
        _isMoving = true;
    }
}
