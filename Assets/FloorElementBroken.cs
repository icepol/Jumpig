using System.Collections;
using pixelook;
using UnityEngine;

public class FloorElementBroken : MonoBehaviour
{
    [SerializeField] float timeForJump = 1;

    private FloorElement _floorElement;
    
    void Awake()
    {
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnPlayerMoveFinished);
    }

    private void Start()
    {
        _floorElement = GetComponent<FloorElement>();
    }

    private void OnDestroy()
    {
        EventManager.AddListener(Events.FLOOR_MOVE_FINISHED, OnPlayerMoveFinished);
    }

    private void OnPlayerMoveFinished()
    {
        Player player = GetComponentInChildren<Player>();
        if (player)
            StartCoroutine(WaitAndFall());
    }

    IEnumerator WaitAndFall()
    {
        yield return new WaitForSeconds(timeForJump);
        
        _floorElement.StartFalling();
    }
}
