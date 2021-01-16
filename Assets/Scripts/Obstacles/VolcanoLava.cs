using UnityEngine;

public class VolcanoLava : MonoBehaviour, IObstacleDangerous
{
    private ParticleSystem _lava;

    private void Awake()
    {
        _lava = GetComponent<ParticleSystem>();
    }

    public void SetDangerousState(bool isDangerous)
    {
        if (isDangerous)
            _lava.Play();
        else
            _lava.Stop();
    }
}
