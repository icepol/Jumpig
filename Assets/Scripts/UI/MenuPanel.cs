using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button soundsButton;
    [SerializeField] private Button musicButton;
    
    private Animator _animator;

    void Start()
    {
        EventManager.AddListener(Events.PLAYER_JUMP_STARTED, OnPlayerJumpStarted);

        _animator = GetComponent<Animator>();
        
        UpdateButton(soundsButton, Settings.IsSfxEnabled);
        UpdateButton(musicButton, Settings.IsMusicEnabled);
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

    public void OnSettingsButtonClick()
    {
        _animator.SetTrigger("ToggleSettings");
    }
    
    public void OnSoundsButtonClick()
    {
        Settings.IsSfxEnabled = !Settings.IsSfxEnabled;
        UpdateButton(soundsButton, Settings.IsSfxEnabled);
    }
    
    public void OnMusicButtonClick()
    {
        Settings.IsMusicEnabled = !Settings.IsMusicEnabled;
        UpdateButton(musicButton, Settings.IsMusicEnabled);
    }

    public void OnLeaderboardButtonClick()
    {
        GameServices.ShowLeaderBoard();
    }

    public void OnRatingButtonClick()
    {
        Application.OpenURL(Constants.AppStoreLink);
    }

    public void OnPrivacyPolicyButtonClick()
    {
        Application.OpenURL(Constants.PrivacyPolicyURL);
    }
    
    void UpdateButton(Button button, bool isEnabled) {
        foreach (Text text in button.GetComponentsInChildren<Text>()) {
            Color color = text.color;
            color.a = isEnabled ? 1f : 0.5f;
            text.color = color;
        }
    }
}
