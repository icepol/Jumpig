using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSetup", menuName = "Assets/Skin Setup")]
public class SkinSetup : ScriptableObject
{
    [Header("Basic Setup")]
    public int coinsForUnlock;
    public bool isUnlocked;
    
    [Header("Visual Setup")]
    public GameObject model;
    
    [Header("Game Services Setup")]
    public string achievementIdAndroid;
    public string achievementIdIos;

    private void OnEnable()
    {
        EventManager.AddListener(Events.COINS_CHANGED, OnCoinsChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.COINS_CHANGED, OnCoinsChanged);
    }

    private void OnCoinsChanged()
    {
        if (isUnlocked || GameState.Coins != coinsForUnlock) return;

        isUnlocked = true;
        
#if UNITY_IPHONE
        GameServices.UnlockAchievement(achievementIdIos);
#elif UNITY_ANDROID
        GameServices.UnlockAchievement(achievementId_Android);
#else
        // will only log the unlock event
        GameServices.UnlockAchievement(achievementIdIos);
#endif
    }
}