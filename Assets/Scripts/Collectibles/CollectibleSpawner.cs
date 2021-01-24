using pixelook;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float collectibleRatio = 0;

    public void Spawn(FloorRow row)
    {
        if (GameState.SpawnedRowsCount < GameManager.Instance.GameSetup.rowsCountToAllowCollectible) return;
        if (collectibleRatio <= 0 || Random.Range(0f, 1f) > collectibleRatio) return;

        FloorElement parentElement = row.FloorElements[Random.Range(0, row.FloorElements.Count)];
        
        if (!parentElement.IsFreeForAddon) return;

        parentElement.IsFreeForAddon = false;

        Collectible[] availableCollectiblePrefabs =
            GameManager.Instance.GameSetup.levels[GameState.Level - 1].availableCollectibles;

        if (availableCollectiblePrefabs.Length == 0) return;

        Instantiate(
            availableCollectiblePrefabs[Random.Range(0, availableCollectiblePrefabs.Length)], 
            parentElement.transform.position,
            Quaternion.identity,
            parentElement.transform);
    }
}
