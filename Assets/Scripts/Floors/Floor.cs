using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private float moveAndSpawnDelay = 1;
    [SerializeField] private int initSpawnCount = 9;
    [SerializeField] private float initSpawnDelay = 0.2f;

    [SerializeField] private float shakeDelay = 3;
    [SerializeField] private float removePlatformsDelay = 1;
    [SerializeField] private float moveRowsDelay = 0.01f;

    private IFloorSpawner _floorSpawner;
    private List<FloorRow> _spawnedRows = new List<FloorRow>();
    private Coroutine _waitMoveAndSpawnCoroutine;
    private bool _isInitialized;
    private bool _isPlayerDead;

    private float _currentSpawnDelay;
    private float _currentMoveRowsDelay;
    
    void Awake()
    {
        _floorSpawner = GetComponent<IFloorSpawner>();
        
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMovementFinished);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
    }

    private void Start()
    {
        EventManager.TriggerEvent(Events.INIT_FLOOR_STARTED);
        
        AddSpawnedRows(GetComponentsInChildren<FloorRow>());
        
        InitializeFloor();
    }

    private void Update()
    {
        if (!_isInitialized)
            InitializeFloor();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMovementFinished);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
        EventManager.RemoveListener(Events.FLOOR_MOVE_FINISHED, OnFloorMoveFinished);
    }
    
    private void OnFloorMoveFinished()
    {
        if (!_isPlayerDead)
            _waitMoveAndSpawnCoroutine = StartCoroutine(WaitMoveAndSpawn());
    }

    private void OnPlayerDied()
    {
        StopMoveCoroutine();
    }

    private void OnPlayerJumpStarted()
    {
        StopMoveCoroutine();
    }

    private void OnPlayerMovementFinished()
    {
        StartCoroutine(MoveAndSpawn());
    }

    void InitializeFloor()
    {
        if (_currentSpawnDelay <= 0)
        {
            _currentSpawnDelay = initSpawnDelay;

            initSpawnCount -= AddSpawnedRows(_floorSpawner.Spawn());
            
            if (initSpawnCount > 0) return;
            
            _isInitialized = true;

            EventManager.TriggerEvent(Events.INIT_FLOOR_FINISHED);
        }
        else
        {
            _currentSpawnDelay -= Time.deltaTime;
        }
    }
    
    IEnumerator WaitMoveAndSpawn()
    {
        yield return new WaitForSeconds(shakeDelay);
        
        // start shaking the first row
        _spawnedRows[0].StartShaking();
        
        yield return new WaitForSeconds(removePlatformsDelay);

        yield return StartCoroutine(MoveAndSpawn());
    }
    
    IEnumerator MoveAndSpawn()
    {
        // fall down
        EventManager.TriggerEvent(Events.FLOOR_FALL_STARTED);
        
        // add new item at the end
        AddSpawnedRows(_floorSpawner.Spawn());
        
        FloorRow firstRow = _spawnedRows[0];
        firstRow.StartFalling();
        
        // remove first row
        _spawnedRows.RemoveAt(0);
        
        yield return new WaitForSeconds(moveAndSpawnDelay);
        
        EventManager.TriggerEvent(Events.FLOOR_MOVE_STARTED);

        _currentMoveRowsDelay = moveRowsDelay;
        foreach (FloorRow row in _spawnedRows)
        {
            row.IsLast = false;
            row.StartMovingForward();

            if (!(moveRowsDelay > 0)) continue;
            
            _currentMoveRowsDelay = 0.6f * _currentMoveRowsDelay;
            yield return new WaitForSeconds(_currentMoveRowsDelay);
        }

        _spawnedRows[_spawnedRows.Count - 1].IsLast = true;
        
        Destroy(firstRow.gameObject);
    }

    private int AddSpawnedRows(FloorRow[] spawnedRows)
    {
        foreach (FloorRow row in spawnedRows)
        {
            _spawnedRows.Add(row);
        }

        return spawnedRows.Length;
    }
    
    private void StopMoveCoroutine()
    {
        if (_waitMoveAndSpawnCoroutine != null)
            StopCoroutine(_waitMoveAndSpawnCoroutine);
    }
}
