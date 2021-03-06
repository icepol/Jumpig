using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetup", menuName = "Assets/Game Setup")]
public class GameSetup : ScriptableObject, IResetBeforeBuild
{
    [Header("Skin Settings")]
    public int selectedSkinIndex = 0;
    public bool areUnlockedAll = false;
    
    public SkinSetup[] skins;
    
    [Header("Floor Settings")]
    public int floorVisibleRowsCount = 30;
    
    [Header("Spawn Settings")]
    public int floorMinRowsCountToSpawnObstacles = 5;
    public int floorMinRowsCountToSpawnCollectibles = 5;
    
    [Header("Row Behaviour Settings")]
    public float rowDelayBeforeShaking = 2;
    public float rowDelayBeforeFalling = 1;

    [Header("Background Settings")]
    public Color[] cameraBackgroundColors;
    
    [Header("Levels Settings")]
    public LevelSetup[] levels;
    
    [Header("Build setup")]
    public bool isProduction = false;

    public bool IsLastRowInLevel
    {
        get
        {
            int rowsCountInPreviousLevels = 0;

            for (int i = 0; i < LevelBySpawnedRows - 1; i++)
            {
                rowsCountInPreviousLevels += levels[i].numberOfRows;
            }

            return GameState.SpawnedRowsCount - rowsCountInPreviousLevels == levels[LevelBySpawnedRows - 1].numberOfRows;
        }
    }

    public int LevelBySpawnedRows
    {
        get
        {
            int rows = 0;
            int levelNumber = 0;

            foreach (LevelSetup levelSetup in levels)
            {
                levelNumber++;
                rows += levelSetup.numberOfRows;

                if (GameState.SpawnedRowsCount <= rows)
                    // this is the level we are currently spawning for
                    break;
            }

            return levelNumber;
        }
    }

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;
        
        selectedSkinIndex = 0;
        areUnlockedAll = false;
    }
}
