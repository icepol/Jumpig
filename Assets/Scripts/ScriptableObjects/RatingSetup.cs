using UnityEngine;

[CreateAssetMenu(fileName = "RatingSetup", menuName = "Assets/Rating Setup")]
public class RatingSetup : ScriptableObject
{
    [Header("Current state")]
    public int attemptsCount = 0;
    public int finishedGames = 0;
    public bool isRated = false;
    
    [Header("Rating setup")]
    public int finishedGamesToShow = 5;
    public int maxAttemptsCount = 3;
    
    [Header("Build setup")]
    public bool isProduction = false;

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;
        
        attemptsCount = 0;
        finishedGames = 0;
        isRated = false;
    }
}
