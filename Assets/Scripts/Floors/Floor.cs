using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private IFloorSpawner _floorSpawner;
    private List<FloorRow> _spawnedRows = new List<FloorRow>();

    public int RowsCount => _spawnedRows.Count;

    void Awake()
    {
        _floorSpawner = GetComponent<IFloorSpawner>();
    }
    
    private void Start()
    {
        EventManager.TriggerEvent(Events.INIT_FLOOR_STARTED);
        
        AddRows(GetComponentsInChildren<FloorRow>());
        
        _floorSpawner.Spawn();
        
        EventManager.TriggerEvent(Events.INIT_FLOOR_FINISHED);
    }

    public void AddRows(FloorRow[] rows)
    {
        foreach (FloorRow row in rows)
        {
            _spawnedRows.Add(row);
        }
    }

    public void RemoveRow()
    {
        _spawnedRows.RemoveAt(0);
        
        EventManager.TriggerEvent(Events.FLOOR_ROW_REMOVED);
    }
}
