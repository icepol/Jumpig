using System.Collections.Generic;
using System.Linq;
using pixelook;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float obstacleRatio = 0;

    public void Spawn(FloorRow row)
    {
        if (obstacleRatio <= 0 || Random.Range(0f, 1f) > obstacleRatio) return;

        FloorElement parentElement = row.FloorElements[Random.Range(0, row.FloorElements.Count)];
        
        if (!parentElement.IsFreeForAddon) return;

        parentElement.IsFreeForAddon = false;

        Obstacle[] availableCollectiblePrefabs =
            GameManager.Instance.GameSetup.levels[GameState.Level].availableObstacles;

        if (availableCollectiblePrefabs.Length == 0) return;

        Instantiate(
            availableCollectiblePrefabs[Random.Range(0, availableCollectiblePrefabs.Length)], 
            parentElement.transform.position,
            Quaternion.identity,
            parentElement.transform);
    }
}