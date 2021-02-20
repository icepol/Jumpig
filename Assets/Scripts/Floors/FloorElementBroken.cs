using System.Collections;
using pixelook;
using UnityEngine;

public class FloorElementBroken : MonoBehaviour
{
    [SerializeField] float timeForJump = 1;

    private DelayAnimation _delayAnimation;
    private FloorElement _floorElement;
    
    void Awake()
    {
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
    }

    private void Start()
    {
        _delayAnimation = GetComponent<DelayAnimation>();
        _delayAnimation.enabled = true;
        
        _floorElement = GetComponent<FloorElement>();
    }

    private void OnDestroy()
    {
        EventManager.AddListener(Events.PLAYER_MOVEMENT_FINISHED, OnPlayerMoveFinished);
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
