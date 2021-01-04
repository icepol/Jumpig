using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public struct UserScore {
    public string userName;
    public float value;

    public UserScore(float value, string userName) {
        this.value = value;
        this.userName = userName;
    }
}

public class GameServices : MonoBehaviour {

    static bool isInitialized;
    static bool showAuthentication = true;
    static bool isAuthenticated = false;

    public static void Initialize() {
        if (isInitialized)
            return;

#if UNITY_ANDROID
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.DebugLogEnabled = true;
#endif

        if (showAuthentication) {
            showAuthentication = false;
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

        isInitialized = true;
    }

    public static void ShowLeaderBoard() {
        if (!isAuthenticated) {
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

        Social.ShowLeaderboardUI();
    }

    public static void ReportScore(string boardId, int score) {
        if (Application.isEditor) {
            Debug.Log("GameServices: Report score " + score + " to board " + boardId);
            return;
        }

        if (isAuthenticated) {
            Social.ReportScore(
                score, boardId, OnReportScore
            );
        }
    }

    private static void OnUserAuthenticated(bool obj) {
        isAuthenticated = true;
    }

    private static void OnReportScore(bool obj) {
    }
}
