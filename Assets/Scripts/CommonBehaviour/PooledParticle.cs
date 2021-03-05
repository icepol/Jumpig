using UnityEngine;

public class PooledParticle : MonoBehaviour
{
    void OnParticleSystemStopped()
    {
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }
}
