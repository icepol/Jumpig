using System;
using System.Collections.Generic;
using System.Linq;
using pixelook;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class FloorSpawner : MonoBehaviour, IFloorSpawner
{
    [SerializeField] private float startSpawningPosition = 1;
    [SerializeField] private List<GameObject> floorGroups;

    private float _nextPosition;
    private IFloorGroup _lastGroup;

    private void Awake()
    {
        _nextPosition = startSpawningPosition;
    }

    private void Start()
    {
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
        List<GameObject> availableFloorGroups =
            floorGroups.Where(item => item.GetComponent<IFloorGroup>().SpawnSetup.IsAvailable).ToList();
        
        while (true)
        {
            IFloorGroup floorGroup = availableFloorGroups[Random.Range(0, availableFloorGroups.Count)]
                .GetComponent<IFloorGroup>();
            
            if (_lastGroup == floorGroup) continue;
            
            _lastGroup = floorGroup;
            return floorGroup;
        }
    }
}
