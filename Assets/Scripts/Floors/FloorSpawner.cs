using pixelook;
using UnityEngine;

public class FloorSpawner : MonoBehaviour, IFloorSpawner
{
    [SerializeField] private float startSpawningPosition = 1;
    [SerializeField] private FloorMovingWrapper movingWrapper; 

    private float _nextPosition;
    private IFloorGroup _lastGroup;
    private Floor _floor;
    private FloorMovingWrapper _floorMovingWrapper;

    private void Awake()
    {
        _nextPosition = startSpawningPosition;

        _floor = GetComponent<Floor>();
        _floorMovingWrapper = GetComponentInChildren<FloorMovingWrapper>();
    }

    private void Start()
    {
        EventManager.AddListener(Events.FLOOR_ROW_REMOVED, OnFloorRowRemoved);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.FLOOR_ROW_REMOVED, OnFloorRowRemoved);
    }

    private void OnFloorRowRemoved()
    {
        Spawn();
    }

    private FloorRow[] SpawnRow()
    {
        IFloorGroup floorGroupToSpawn = NextToSpawn();

        GameObject instance =
            Instantiate(((MonoBehaviour) floorGroupToSpawn).gameObject, movingWrapper.transform, false);
        
        instance.transform.localPosition = new Vector3(0, 0, _nextPosition);

        IFloorGroup floorGroup = instance.GetComponent<IFloorGroup>();
        _nextPosition += floorGroup.Size();

        return floorGroup.Rows();
    }

    private IFloorGroup NextToSpawn()
    {
        IFloorGroup[] availableFloorGroups = GameManager.Instance.GameSetup
            .levels[GameManager.Instance.GameSetup.LevelBySpawnedRows - 1].availableFloors;

        while (true)
        {
            IFloorGroup floorGroup = availableFloorGroups[Random.Range(0, availableFloorGroups.Length)];

            if (_lastGroup == floorGroup) continue;
            
            _lastGroup = floorGroup;
            return floorGroup;
        }
    }
    
    public void Spawn()
    {
        while (_floor.RowsCount < GameManager.Instance.GameSetup.floorVisibleRowsCount)
        {
            _floor.AddRows(SpawnRow());
        }
    }
}
