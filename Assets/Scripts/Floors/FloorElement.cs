using pixelook;
using UnityEngine;

public class FloorElement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private FloorRow _floorRow;

    private bool _isFalling;
    
    public bool IsFreeForAddon { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _floorRow = GetComponentInParent<FloorRow>();

        IsFreeForAddon = true;
    }

    public void OnPlayerCollision()
    {
        _floorRow.OnPlayerCollision();
    }

    public void StartShaking()
    {
        _animator.SetTrigger("IsShaking");
    }

    public void StartFalling()
    {
        if (_isFalling) return;

        _isFalling = true;
        
        _boxCollider.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.down * Random.Range(100f, 150f));

        Player player = GetComponentInChildren<Player>();
        if (player)
        {
            EventManager.TriggerEvent(Events.PLAYER_DIED);
            EventManager.TriggerEvent(Events.PLAYER_FALLEN);
        }
    }
}
