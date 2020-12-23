using System.Collections;
using System.Collections.Generic;
using pixelook;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.SCORE_CHANGED, OnScoreChanged);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.RemoveListener(Events.SCORE_CHANGED, OnScoreChanged);
    }
    
    private void OnScoreChanged()
    {
        _animator.SetTrigger("ScoreChanged");
    }
}
