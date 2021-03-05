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
        
        if (collectedEffect != null)
        {
            GameObject effectInstance = ObjectPoolManager.Instance.GetFromPool(collectedEffect.gameObject);
            effectInstance.transform.position = transform.position;
        }
        
        GameObject instance = ObjectPoolManager.Instance.GetFromPool(scoreBalloon.gameObject);
        instance.transform.position = transform.position;
        instance.GetComponent<ScoreBalloon>().SetScore(scorePoints * GameState.ComboMultiplier);

        GameState.Score += scorePoints * GameState.ComboMultiplier;
        
        if (postCollectEvent != null)
            EventManager.TriggerEvent(postCollectEvent);
        
        Destroy(gameObject);
    }
}
