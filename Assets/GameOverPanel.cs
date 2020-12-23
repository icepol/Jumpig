using pixelook;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private Vector2 _position;
    
    void Start()
    {
        // move off the screen
        _position = transform.position;
        transform.position = new Vector3(10000, 10000, 0);
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        transform.position = _position;
    }
}
