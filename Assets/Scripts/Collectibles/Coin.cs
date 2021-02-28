using pixelook;
using UnityEngine;

public class Coin : MonoBehaviour, ICollisionHandler
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() == null) return;
        
        GameState.Coins++;
    }
}
