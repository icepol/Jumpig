using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private List<Collectible> collectiblePrefabs;
    [SerializeField, Range(0, 1)] private float collectibleRatio = 0;

    public void Spawn(FloorRow row)
    {
        if (collectibleRatio <= 0 || Random.Range(0f, 1f) > collectibleRatio) return;

        FloorElement parentElement = row.FloorElements[Random.Range(0, row.FloorElements.Count)];
        
        if (!parentElement.IsFreeForAddon) return;

        parentElement.IsFreeForAddon = false;

        List<Collectible> availableCollectiblePrefabs =
            collectiblePrefabs.Where(item => item.SpawnSetup.IsAvailable).ToList();
        
        if (availableCollectiblePrefabs.Count == 0) return;

        Instantiate(
            availableCollectiblePrefabs[Random.Range(0, availableCollectiblePrefabs.Count)], 
            parentElement.transform.position,
            Quaternion.identity,
            parentElement.transform);
    }
}
