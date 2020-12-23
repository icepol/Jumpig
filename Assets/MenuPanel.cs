using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    private bool _isHidden;

    void Start()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
    }

    private void OnPlayerJumpStarted()
    {
        gameObject.SetActive(false);
    }
}
