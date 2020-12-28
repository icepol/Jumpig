using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    private ICollisionHandler _collisionHandler;

    private void Awake()
    {
        _collisionHandler = GetComponentInParent<ICollisionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionHandler.OnTriggerEnter(other);
    }
}
