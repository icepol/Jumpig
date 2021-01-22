using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetup", menuName = "Assets/Level Setup")]
public class LevelSetup : ScriptableObject
{
    public FloorRowGroup[] availableFloors;
    public Collectible[] availableCollectibles;
    public Obstacle[] availableObstacles;
}
