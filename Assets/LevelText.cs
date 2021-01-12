using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private Text _text;
    private Animator _animator;
    
    void Start()
    {
        _text = GetComponent<Text>();
        _animator = GetComponent<Animator>();
        
        EventManager.AddListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.RemoveListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }
    
    private void OnLevelChanged()
    {
        _text.text = $"Level {GameState.Level}";
        _animator.SetTrigger("LevelChanged");
    }
}