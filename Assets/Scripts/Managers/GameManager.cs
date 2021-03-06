using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameSetup gameSetup;
    
    private bool _isGameRunning;

    public static GameManager Instance { get; private set; }
    public GameSetup GameSetup => gameSetup;
    
    private void Awake()
    {
        Instance = this;

        GameState.SpawnedRowsCount = 0;
        
        LoadFromFile();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.AddListener(Events.LEVEL_CHANGED, OnLevelChanged);
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void Start()
    {
        GameAnalytics.Initialize();
        GameServices.Initialize();
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
        EventManager.RemoveListener(Events.LEVEL_CHANGED, OnLevelChanged);
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void LoadFromFile()
    {
        GameSetup loadedGameSetup = ScriptableObject.CreateInstance<GameSetup>();
        loadedGameSetup.LoadFromFile();

        GameSetup.areUnlockedAll = loadedGameSetup.areUnlockedAll;
        GameSetup.selectedSkinIndex = loadedGameSetup.selectedSkinIndex;

        SkinSetup loadedSkinSetup = ScriptableObject.CreateInstance<SkinSetup>();

        foreach (SkinSetup skinSetup in GameSetup.skins)
        {
            if (!skinSetup.isPersistent) continue;
            
            loadedSkinSetup.skinName = skinSetup.skinName;
            loadedSkinSetup.isPersistent = skinSetup.isPersistent;
            loadedSkinSetup.LoadFromFile();

            skinSetup.IsUnlocked = loadedSkinSetup.IsUnlocked;
        }
        
        Destroy(loadedSkinSetup);
        Destroy(loadedGameSetup);
    }

    private void OnPlayerJumpStarted()
    {
        if (_isGameRunning) return;
        
        _isGameRunning = true;
        
        EventManager.TriggerEvent(Events.GAME_STARTED);
    }

    private void OnGameStarted()
    {
        GameState.Reset();
    }

    private void OnLevelChanged()
    {
        if (GameState.Level > 1)
            // we completed the previous level
            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Complete, 
                "World_1", 
                $"Level_{GameState.Level - 1}");
        
        GameAnalytics.NewProgressionEvent(
            GAProgressionStatus.Start, 
            "World_1", 
            $"Level_{GameState.Level}");
    }

    private void OnPlayerDied()
    {
        GameAnalytics.NewProgressionEvent(
            GAProgressionStatus.Fail, 
            "World_1", 
            $"Level_{GameState.Level}",
            GameState.Score);
        
        GameServices.ReportScore(Constants.TopScoreLeaderBoardId, GameState.Score);
        GameServices.ReportScore(Constants.TopDistanceReachedId, GameState.Distance);

        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2.5f);
        
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
