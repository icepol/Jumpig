using pixelook;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.INIT_FLOOR_STARTED, OnInitFloorStarted);
        EventManager.AddListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_STARTED, OnInitFloorStarted);
        EventManager.RemoveListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
    }

    private void OnInitFloorStarted()
    {
        _animator.SetTrigger("Move");
    }

    private void OnFloorMoveStarted()
    {
        _animator.SetTrigger("Move");
    }
}
