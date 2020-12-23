using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _floorLayerMask;

    private void Awake()
    {
        _floorLayerMask = 1 << LayerMask.NameToLayer("Floor");
    }

    void Start()
    {
        EventManager.AddListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.AddListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.INIT_FLOOR_FINISHED, OnInitFloorFinished);
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }
    
    private void OnPlayerJumpFinished()
    {
        // TODO: check target floor
        
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
        }
    }
}