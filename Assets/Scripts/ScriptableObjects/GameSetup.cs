using UnityEngine;

[CreateAssetMenu(fileName = "GameSetup", menuName = "Assets/Game Setup")]
public class GameSetup : ScriptableObject
{
    public int rowsCountToAllowCollectible = 15;
    public int rowsPerLevel = 50;
    
    public LevelSetup[] levels;
}
