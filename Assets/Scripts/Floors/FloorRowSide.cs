using UnityEngine;

public class FloorRowSide : MonoBehaviour
{
    [SerializeField] private int offset = 1;
    
    void Start()
    {
        transform.localPosition += Vector3.right * Random.Range(-offset, offset + 1);
    }
}
