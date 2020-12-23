using pixelook;
using UnityEngine;

public class FloorElement : MonoBehaviour
{
    private DelayAnimation _delayAnimation;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _delayAnimation = GetComponent<DelayAnimation>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        
        _delayAnimation.enabled = false;
    }

    public void StartShaking()
    {
        _delayAnimation.enabled = true;
    }

    public void StartFalling()
    {
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.down * Random.Range(100f, 150f));

        Player player = GetComponentInChildren<Player>();
        if (player)
            EventManager.TriggerEvent(Events.PLAYER_DIED);
    }
}
