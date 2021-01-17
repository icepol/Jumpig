using pixelook;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private ParticleSystem onCollisionParticle;
    
    private Animator _animator;
    private IObstacleDangerous[] _obstacleDangerous;
    private bool _isDangerous;
    
    public bool IsDangerous
    {
        get => _isDangerous;
        
        set
        {
            _isDangerous = value;
            
            if (_animator != null)
                _animator.SetBool("IsDangerous", _isDangerous);
            
            foreach (IObstacleDangerous obstacleDangerous in _obstacleDangerous)
                obstacleDangerous.SetDangerousState(IsDangerous);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _obstacleDangerous = GetComponentsInChildren<IObstacleDangerous>();
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