using pixelook;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private int scorePoints = 10;
    [SerializeField] private ParticleSystem collectedEffect;
    
    public void OnTriggerEnter(Collider other)
    {
        Instantiate(collectedEffect, transform.position, Quaternion.identity);

        GameState.Score += scorePoints;
        
        Destroy(gameObject);
    }
}
