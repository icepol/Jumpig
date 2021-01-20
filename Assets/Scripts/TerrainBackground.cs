using pixelook;
using UnityEngine;

public class TerrainBackground : MonoBehaviour
{
    private Camera _terrainCamera;

    private void Awake()
    {
        _terrainCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        _terrainCamera.backgroundColor = GameManager.Instance.GameSetup.cameraBackgroundColors[0];
        
        EventManager.AddListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }

    private void OnLevelChanged()
    {
        _terrainCamera.backgroundColor = GameManager.Instance.GameSetup.cameraBackgroundColors[
            (GameState.Level - 1) % GameManager.Instance.GameSetup.cameraBackgroundColors.Length
        ];
    }
}
