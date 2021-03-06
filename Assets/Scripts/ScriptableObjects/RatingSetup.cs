using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "RatingSetup", menuName = "Assets/Rating Setup")]
public class RatingSetup : ScriptableObject
{
    [Header("Current state")]
    public int attemptsCount = 0;
    public int finishedGames = 0;
    public bool isRated = false;
    
    [Header("Setup")]
    public int finishedGamesToShow = 5;
    public int maxAttemptsCount = 3;
}
