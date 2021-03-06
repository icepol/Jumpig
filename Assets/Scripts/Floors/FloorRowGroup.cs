using System;
using pixelook;
using UnityEngine;

public class FloorRowGroup : MonoBehaviour, IFloorGroup
{
    [SerializeField] private int size = 1;

    private FloorRow[] _floorRows;
    
    void Awake()
    {
        FetchRows();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void OnPlayerMoveFinished(Vector3 position)
    {
        FetchRows();
    }

    public int Size()
    {
        return size;
    }

    public FloorRow[] Rows()
    {
        return _floorRows;
    }

    private void FetchRows()
    {
        _floorRows = GetComponentsInChildren<FloorRow>();
        
        if (_floorRows.Length == 0)
            Destroy(gameObject);
    }
}
