using pixelook;
using UnityEngine;

public class DangerousOnJumpFinished : MonoBehaviour
{
    [SerializeField] private bool randomStartDangerous;
    
    private Obstacle _obstacle;
    
    private void Awake()
    {
        _obstacle = GetComponent<Obstacle>();
    }

    void Start()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
        
        if (randomStartDangerous)
            _obstacle.IsDangerous = Random.Range(0f, 1f) > 0.5f;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnPlayerJumpFinished()
    {
        _obstacle.IsDangerous = !_obstacle.IsDangerous;
    }
}