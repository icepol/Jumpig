using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "Assets/Level Setup")]
public class LevelSetup : ScriptableObject
{
    public FloorRow[] availableFloors;
    public Collectible[] availableCollectibles;
    public Obstacle[] availableObstacles;
}
