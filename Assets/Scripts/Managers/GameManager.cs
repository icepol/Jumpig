using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    private bool _isGameRunning;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        GameAnalytics.Initialize();
        GameServices.Initialize();
        
        GameState.SpawnedRowsCount = 0;
        
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerJumpStarted()
    {
        if (_isGameRunning) return;
        
        _isGameRunning = true;
        GameState.Reset();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "World_1");
        
        EventManager.TriggerEvent(Events.LEVEL_STARTED);
    }

    private void OnPlayerDied()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "World_1", GameState.Score);
        
        GameServices.ReportScore(Constants.TopScoreLeaderBoardId, GameState.Score);
        GameServices.ReportScore(Constants.TopDistanceReachedId, GameState.Distance);

        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Game");
    }
}
