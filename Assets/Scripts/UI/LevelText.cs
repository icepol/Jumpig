using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private Text _text;
    private Animator _animator;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.LEVEL_CHANGED, OnLevelChanged);
    }
    
    private void OnLevelChanged()
    {
        _text.text = $"Level {GameState.Level}";
        _animator.SetTrigger("LevelChanged");
    }
}