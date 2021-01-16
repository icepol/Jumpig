using pixelook;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private SpawnSetup spawnSetup;
    [SerializeField] private ParticleSystem onCollisionParticle;
    
    private Animator _animator;
    public bool _isDangerous;
    
    public SpawnSetup SpawnSetup => spawnSetup;

    public bool IsDangerous
    {
        get => _isDangerous;
        
        set
        {
            _isDangerous = value;
            
            if (_animator != null)
                _animator.SetBool("IsDangerous", _isDangerous);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;
        if (!IsDangerous) return;
        
        if (onCollisionParticle != null)
            Instantiate(onCollisionParticle, transform.position, Quaternion.identity);

        EventManager.TriggerEvent(Events.PLAYER_DIED);
        EventManager.TriggerEvent(Events.PLAYER_COLLIDED_OBSTACLE);
    }
}