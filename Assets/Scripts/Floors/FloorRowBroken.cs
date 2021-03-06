using UnityEngine;
using Random = UnityEngine.Random;

public class FloorRowBroken : MonoBehaviour
{
    [SerializeField] private FloorElement brokenElementPrefab;
    [SerializeField] private int broken = 1;

    private FloorRow _floorRow;
    private FloorElement[] _floorElements;

    private void Awake()
    {
        _floorRow = GetComponent<FloorRow>();
        _floorElements = GetComponentsInChildren<FloorElement>();
    }

    void Start()
    {
        while (broken > 0)
        {
            GameObject element = _floorElements[Random.Range(0, _floorElements.Length)].gameObject;
            if (!element.activeSelf) continue;

            FloorElement floorElement =
                Instantiate(brokenElementPrefab, element.transform.position, Quaternion.identity, transform);
            
            _floorRow.FloorElements.Add(floorElement);

            element.SetActive(false);
            broken--;
        }
    }
}
