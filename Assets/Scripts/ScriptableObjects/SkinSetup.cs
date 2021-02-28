using UnityEngine;

[CreateAssetMenu(fileName = "SkinSetup", menuName = "Assets/Skin Setup")]
public class SkinSetup : ScriptableObject
{
    public int coinsForUnlock;
    public bool isUnlocked;
    
    public GameObject model;
}