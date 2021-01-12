using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName="SpawnSetup", menuName="Assets/Spawn setup")]
public class SpawnSetup : ScriptableObject
{
    public int availableFromScore = -1;
    public int availableFromRow = -1;
    public int availableFromLevel = -1;

    private int LevelBasedOnRows => GameState.SpawnedRowsCount / Constants.IncreaseLevelEachRowsCount + 1;

    public bool IsAvailable =>
        (availableFromScore < 0 ||
        availableFromScore >= 0 && GameState.Score >= availableFromScore) &&
        (availableFromRow < 0 ||
        availableFromRow >= 0 && GameState.SpawnedRowsCount >= availableFromRow) &&
        (availableFromLevel < 0 ||
         availableFromLevel >= 0 && LevelBasedOnRows >= availableFromLevel);
}
