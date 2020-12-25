using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private Collectible[] collectiblePrefabs;
    [SerializeField, Range(0, 1)] private float collectibleRatio = 0;

    public void Spawn(FloorRow row)
    {
        if (collectibleRatio <= 0 || Random.Range(0f, 1f) > collectibleRatio) return;

        FloorElement parentElement = row.FloorElements[Random.Range(0, row.FloorElements.Length)];
        
        Instantiate(
            collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)], 
            parentElement.transform.position,
            Quaternion.identity,
            parentElement.transform);
    }
}
