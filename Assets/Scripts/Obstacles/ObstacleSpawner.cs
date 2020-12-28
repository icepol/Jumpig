using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Obstacle> obstaclePrefabs;
    [SerializeField, Range(0, 1)] private float obstacleRatio = 0;

    public void Spawn(FloorRow row)
    {
        if (obstacleRatio <= 0 || Random.Range(0f, 1f) > obstacleRatio) return;

        FloorElement parentElement = row.FloorElements[Random.Range(0, row.FloorElements.Count)];
        
        if (!parentElement.IsFreeForAddon) return;

        parentElement.IsFreeForAddon = false;

        List<Obstacle> availableCollectiblePrefabs =
            obstaclePrefabs.Where(item => item.SpawnSetup.IsAvailable).ToList();
        
        if (availableCollectiblePrefabs.Count == 0) return;

        Instantiate(
            availableCollectiblePrefabs[Random.Range(0, availableCollectiblePrefabs.Count)], 
            parentElement.transform.position,
            Quaternion.identity,
            parentElement.transform);
    }
}