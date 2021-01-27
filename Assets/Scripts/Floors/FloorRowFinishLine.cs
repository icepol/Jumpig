using UnityEngine;

public class FloorRowFinishLine : MonoBehaviour
{
    [SerializeField] private FinishLine finishLine;
    
    public void Spawn()
    {
        if (GameManager.Instance.GameSetup.IsLastRowInLevel)
            SpawnFinishLine();
    }

    private void SpawnFinishLine()
    {
        FinishLine finishLineInstance = Instantiate(finishLine, transform.position, Quaternion.identity, transform);
        finishLineInstance.SetLevelNumber((GameManager.Instance.GameSetup.LevelBySpawnedRows + 1).ToString());
    }
}
