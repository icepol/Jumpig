using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Transform rightPillar;
    [SerializeField] private Transform flagBackground;
    [SerializeField] private TextMesh levelText;

    private FloorRow _floorRow;

    private void Awake()
    {
        _floorRow = GetComponentInParent<FloorRow>();
    }

    void Start()
    {
        SetPositionAndSize();
    }
    
    private void SetPositionAndSize()
    {
        List<FloorElement> sortedFloorElements =
            _floorRow.FloorElements.OrderBy(item => item.transform.position.x).ToList();
        
        Vector3 firstElementPosition = sortedFloorElements[0].transform.localPosition;
        Vector3 lastElementPosition = sortedFloorElements[_floorRow.FloorElements.Count - 1].transform.localPosition;

        float rowWidth = lastElementPosition.x - firstElementPosition.x + 1;

        // move whole group to the starting position
        transform.localPosition += Vector3.right * (firstElementPosition.x - 1);
        
        // move right pillar to the ending position 
        rightPillar.localPosition += Vector3.right * rowWidth;

        // set the flag to right position and scale
        flagBackground.transform.localScale += Vector3.right * rowWidth;
        flagBackground.transform.localPosition += Vector3.right * rowWidth * 0.5f;

        // set the text to the right position
        levelText.transform.localPosition += Vector3.right * rowWidth * 0.5f;
    }

    public void SetLevelNumber(string level)
    {
        levelText.text = "Finish";
    }
}
