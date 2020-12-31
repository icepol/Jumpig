using System.Collections.Generic;
using System.Linq;
using pixelook;
using UnityEngine;

public class FloorRow : MonoBehaviour, IFloorGroup
{
    [SerializeField] private SpawnSetup spawnSetup;
    [SerializeField] private float moveTime = 0.5f;
    
    private int size = 1;

    private CollectibleSpawner _collectibleSpawner;
    private ObstacleSpawner _obstacleSpawner;

    private bool _isMovingForward;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private float _currentTime;

    public List<FloorElement> FloorElements { get; private set; }
    public SpawnSetup SpawnSetup => spawnSetup;
    public bool IsLast { get; set; }

    public void Awake()
    {
        FloorElements = GetComponentsInChildren<FloorElement>().ToList();
        
        _collectibleSpawner = GetComponent<CollectibleSpawner>();
        _obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    public void Start()
    {
        GameState.SpawnedRowsCount += 1;

        if (_collectibleSpawner != null)
            _collectibleSpawner.Spawn(this);
        
        if (_obstacleSpawner != null)
            _obstacleSpawner.Spawn(this);
    }
    
    public void Update()
    {
        if (!_isMovingForward) return;
        
        transform.position = Vector3.Lerp(_oldPosition, _newPosition, _currentTime / moveTime);

        _currentTime += Time.deltaTime;

        if (transform.position != _newPosition) return;
        
        _isMovingForward = false;
        
        if (IsLast)
            EventManager.TriggerEvent(Events.FLOOR_MOVE_FINISHED);
    }

    public void StartShaking()
    {
        foreach (var floorElement in FloorElements)
        {
            floorElement.StartShaking();
        }
    }

    public void StartFalling()
    {
        foreach (var floorElement in FloorElements)
        {
            floorElement.StartFalling();
        }
    }

    public void StartMovingForward()
    {
        _oldPosition = transform.position;
        _newPosition = _oldPosition + Vector3.back;

        _currentTime = 0;
        _isMovingForward = true;
    }

    public int Size()
    {
        return size;
    }

    public FloorRow[] Rows()
    {
        return new[] {this};
    }
}
