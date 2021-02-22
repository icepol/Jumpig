using pixelook;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    private int _currentCombo;
    
    void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.AddListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);
        EventManager.RemoveListener(Events.PLAYER_JUMP_FINISHED, OnPlayerJumpFinished);
    }

    private void OnPlayerJumpStarted()
    {
        _currentCombo = GameState.ComboMultiplier;
    }   

    private void OnPlayerJumpFinished()
    {
        if (GameState.ComboMultiplier <= _currentCombo)
        {
            // reset combo multiplier as no collectible was taken this move
            GameState.ComboMultiplier = 0;
        }
    }
}
