using System.Collections;
using pixelook;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        GameState.SpawnedRowsCount = 0;
        
        EventManager.AddListener(Events.PLAYER_DIED, OnPlayerDied);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.RemoveListener(Events.PLAYER_DIED, OnPlayerDied);
    }
    
    private void OnPlayerDied()
    {
        StartCoroutine(WaitAndRestart());
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Game");
    }
}
