using System.Collections;
using GameAnalyticsSDK;
using pixelook;
using UnityEngine;

public class RatePanel : MonoBehaviour
{
    [SerializeField] private RatingSetup ratingSetup;
    [SerializeField] private GameObject elements;

    private void OnEnable()
    {
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    void Start()
    {
        elements.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        ratingSetup.finishedGames++;
        
        if (!IsAvailable()) return;

        GameAnalytics.NewDesignEvent($"rating:show");
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(1f);
        
        Time.timeScale = 0;
        elements.SetActive(true);
        ratingSetup.attemptsCount++;
    }

    private bool IsAvailable()
    {
        return !ratingSetup.isRated && ratingSetup.attemptsCount < ratingSetup.maxAttemptsCount &&
               ratingSetup.finishedGames % ratingSetup.finishedGamesToShow == 0;
    }

    public void OnYesButtonClick()
    {
        GameAnalytics.NewDesignEvent($"rating:response:yes");
        
        elements.SetActive(false);
        Time.timeScale = 1;
        ratingSetup.isRated = true;
        
        Application.OpenURL(Constants.AppStoreLink);
    }

    public void OnNoButtonClick()
    {
        GameAnalytics.NewDesignEvent($"rating:response:no");
        
        elements.SetActive(false);
        Time.timeScale = 1;
    }
}
