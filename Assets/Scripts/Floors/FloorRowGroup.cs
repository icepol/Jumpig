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
        
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnFloorRowFinished);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.FLOOR_MOVE_FINISHED, OnFloorRowFinished);
    }

    private void OnFloorRowFinished()
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
