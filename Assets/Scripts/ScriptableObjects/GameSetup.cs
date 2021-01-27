using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetup", menuName = "Assets/Game Setup")]
public class GameSetup : ScriptableObject
{
    public int rowsCountToAllowSpawning = 15;

    public Color[] cameraBackgroundColors;
    
    public LevelSetup[] levels;

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
}
