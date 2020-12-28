using pixelook;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private SpawnSetup spawnSetup;
    [SerializeField] private ParticleSystem dieEffect;
    
    public SpawnSetup SpawnSetup => spawnSetup;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;
        
        if (dieEffect != null)
            Instantiate(dieEffect, transform.position, Quaternion.identity);

        EventManager.TriggerEvent(Events.PLAYER_DIED);
    }
}