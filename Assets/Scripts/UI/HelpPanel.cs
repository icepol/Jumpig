using pixelook;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    private Animator _animator;
    private bool _isHidden;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        if (_isHidden) return;
        
        _animator.SetTrigger("Hide");
        _isHidden = true;
    }
}
