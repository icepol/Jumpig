using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pixelook;
using UnityEngine;

public class FloorRow : MonoBehaviour, IFloorGroup
{
    public List<FloorElement> FloorElements { get; private set; }

    private int _size = 1;

    private Floor _floor;
    private CollectibleSpawner _collectibleSpawner;
    private ObstacleSpawner _obstacleSpawner;
    private FloorRowFinishLine _floorRowFinishLine;

    public void Awake()
    {
        FloorElements = GetComponentsInChildren<FloorElement>().ToList();

        _floor = GetComponentInParent<Floor>();
        _collectibleSpawner = GetComponent<CollectibleSpawner>();
        _obstacleSpawner = GetComponent<ObstacleSpawner>();
        _floorRowFinishLine = GetComponent<FloorRowFinishLine>();
    }

    public void Start()
    {
        GameState.SpawnedRowsCount += 1;

        if (_collectibleSpawner != null)
            _collectibleSpawner.Spawn(this);
        
        if (_obstacleSpawner != null)
            _obstacleSpawner.Spawn(this);

        if (_floorRowFinishLine != null)
            _floorRowFinishLine.Spawn();
    }

    IEnumerator WaitShakeAndFall()
    {
        yield return new WaitForSeconds(GameManager.Instance.GameSetup.rowDelayBeforeShaking);
            
        foreach (var floorElement in FloorElements)
        {
            floorElement.StartShaking();
        }

        yield return new WaitForSeconds(GameManager.Instance.GameSetup.rowDelayBeforeFalling);
        
        foreach (var floorElement in FloorElements)
        {
            floorElement.StartFalling();
        }
        
        yield return new WaitForSeconds(1.5f);
        
        _floor.RemoveRow();
        
        Destroy(gameObject);
    }

    public int Size()
    {
        return _size;
    }

    public FloorRow[] Rows()
    {
        return new[] {this};
    }

    public void OnPlayerCollision()
    {
        StartCoroutine(WaitShakeAndFall());
    }
}
