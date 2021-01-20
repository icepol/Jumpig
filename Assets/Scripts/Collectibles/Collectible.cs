using pixelook;
using UnityEngine;

public class Collectible : MonoBehaviour, ICollisionHandler
{
    [SerializeField] private int scorePoints = 10;
    [SerializeField] private ParticleSystem collectedEffect;
    [SerializeField] private ScoreBalloon scoreBalloon;
    [SerializeField] private string postCollectEvent;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;

        GameState.ComboMultiplier++;
        
        Instantiate(collectedEffect, transform.position, Quaternion.identity);
        
        ScoreBalloon scoreBalloonInstance = Instantiate(scoreBalloon, transform.position, Quaternion.identity);
        
        scoreBalloonInstance.SetScore(scorePoints * GameState.ComboMultiplier);

        GameState.Score += scorePoints * GameState.ComboMultiplier;
        
        if (postCollectEvent != null)
            EventManager.TriggerEvent(postCollectEvent);
        
        Destroy(gameObject);
    }
}
