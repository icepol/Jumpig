using pixelook;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private SpawnSetup spawnSetup;
    
    [SerializeField] private int scorePoints = 10;
    [SerializeField] private ParticleSystem collectedEffect;
    [SerializeField] private ScoreBalloon scoreBalloon;
    [SerializeField] private string postCollectEvent;
    
    public SpawnSetup SpawnSetup => spawnSetup;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;
        
        Instantiate(collectedEffect, transform.position, Quaternion.identity);
        
        ScoreBalloon scoreBalloonInstance = Instantiate(scoreBalloon, transform.position, Quaternion.identity);
        scoreBalloonInstance.SetScore(scorePoints);

        GameState.Score += scorePoints;
        
        if (postCollectEvent != null)
            EventManager.TriggerEvent(postCollectEvent);
        
        Destroy(gameObject);
    }
}
