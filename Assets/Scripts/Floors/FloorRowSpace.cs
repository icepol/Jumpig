using UnityEngine;

public class FloorRowSpace : MonoBehaviour
{
    [SerializeField] private FloorElement[] floorElements;
    [SerializeField] private int spaces = 1;
    
    void Start()
    {
        while (spaces > 0)
        {
            GameObject element = floorElements[Random.Range(0, floorElements.Length)].gameObject;
            if (!element.activeSelf) continue;
            
            element.SetActive(false);
            spaces--;
        }
    }
}
