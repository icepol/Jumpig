using pixelook;
using UnityEngine;

public class DangerousOnJumpFinished : MonoBehaviour
{
    [SerializeField] private bool randomStartDangerous;
    [SerializeField] private int changeStateJumpsCount = 1;
    
    private Obstacle _obstacle;
    private int _jumps;
    
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
        if (++_jumps < changeStateJumpsCount) return;
        
        _jumps = 0;
        _obstacle.IsDangerous = !_obstacle.IsDangerous;
    }
}