using pixelook;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private SpawnSetup spawnSetup;
    [SerializeField] private ParticleSystem onCollisionParticle;
    
    public SpawnSetup SpawnSetup => spawnSetup;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;
        
        if (onCollisionParticle != null)
            Instantiate(onCollisionParticle, transform.position, Quaternion.identity);

        EventManager.TriggerEvent(Events.PLAYER_DIED);
        EventManager.TriggerEvent(Events.PLAYER_COLLIDED_OBSTACLE);
    }
}