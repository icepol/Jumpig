using pixelook;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.INIT_FLOOR_STARTED, OnInitFloorStarted);
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_STARTED, OnInitFloorStarted);
        EventManager.RemoveListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnInitFloorStarted()
    {
        _animator.SetTrigger("Move");
    }

    private void OnPlayerMoveFinished(Vector3 position)
    {
        _animator.SetTrigger("Move");
    }
}
