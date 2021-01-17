using pixelook;
using UnityEngine;

public class FloorRowFinishLine : MonoBehaviour
{
    [SerializeField] private FinishLine finishLine;
    
    public void Spawn()
    {
        if (GameState.SpawnedRowsCount % GameManager.Instance.GameSetup.rowsPerLevel == 0)
            SpawnFinishLine();
    }

    private void SpawnFinishLine()
    {
        FinishLine finishLineInstance = Instantiate(finishLine, transform.position, Quaternion.identity, transform);
        finishLineInstance.SetLevelNumber((
            GameState.SpawnedRowsCount / GameManager.Instance.GameSetup.rowsPerLevel + 1
            ).ToString());
    }
}
