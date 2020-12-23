using UnityEngine;

public class FloorRowSide : MonoBehaviour
{
    [SerializeField] private int offset = 1;
    
    void Start()
    {
        Vector3 position = transform.position;
        position = new Vector3(Random.Range(-offset, offset), position.y, position.z);
        
        transform.position = position;
    }
}
