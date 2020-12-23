using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class FloorSpawner : MonoBehaviour, IFloorSpawner
{
    [SerializeField] private float startSpawningPosition = 1;
    [SerializeField] private GameObject[] floorGroups;

    private float _nextPosition;
    private IFloorGroup _lastGroup;

    private void Start()
    {
        _nextPosition = startSpawningPosition;
        
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
    }

    private void OnFloorMoveFinished()
    {
        // decrease next position as floor moved
        _nextPosition--;
    }

    public FloorRow[] Spawn()
    {
        IFloorGroup floorGroup = NextToSpawn();

        IFloorGroup instance = (IFloorGroup)Instantiate(
            (Object)floorGroup,
            new Vector3(0, 0, _nextPosition),
            Quaternion.identity,
            transform);

        _nextPosition += instance.Size();

        return instance.Rows();
    }

    IFloorGroup NextToSpawn()
    {
        while (true)
        {
            IFloorGroup floorGroup = floorGroups[Random.Range(0, floorGroups.Length)].GetComponent<IFloorGroup>();
            if (_lastGroup == floorGroup) continue;
            
            _lastGroup = floorGroup;
            return floorGroup;
        }
    }
}
