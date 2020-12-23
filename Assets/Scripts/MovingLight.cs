using pixelook;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    private Animator _animator;
    private bool _isFloorInitialized;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.AddListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.RemoveListener(Events.FLOOR_MOVE_STARTED, OnFloorMoveStarted);
    }

    private void OnInitFloorFinished()
    {
        _isFloorInitialized = true;
    }

    private void OnFloorMoveStarted()
    {
        if (!_isFloorInitialized)
            return;
        
        _animator.SetTrigger("Move");
    }
}
