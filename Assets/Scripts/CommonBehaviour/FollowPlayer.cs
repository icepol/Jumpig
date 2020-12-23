using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (_player == null) return;
        
        transform.position = new Vector3(
            _player.transform.position.x, 
            transform.position.y, 
            transform.position.z);
    }
}
