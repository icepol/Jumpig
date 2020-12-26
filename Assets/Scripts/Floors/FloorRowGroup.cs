using UnityEngine;

public class FloorRowGroup : MonoBehaviour, IFloorGroup
{
    [SerializeField] private SpawnSetup spawnSetup;
    [SerializeField] private int size = 1;

    public SpawnSetup SpawnSetup => spawnSetup;

    private FloorRow[] _floorRows;
    
    void Awake()
    {
        _floorRows = GetComponentsInChildren<FloorRow>();
    }

    public int Size()
    {
        return size;
    }

    public FloorRow[] Rows()
    {
        return _floorRows;
    }
}
