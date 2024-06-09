using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Configs/Levels/New-Level", order = 0)]
public class Level : ScriptableObject
{
    
#if UNITY_EDITOR
    [TextArea(1,1)] public string info = "Number of cards are required to be selected to make Match?";
    private void OnValidate()
    {
        requiredCardPerMatchCount = (requiredCardPerMatchCount < 2) ? 2 : requiredCardPerMatchCount;
        
        if (levelImages != null && levelImages.Length > 0)
            MaxTurnsCount = (levelImages != null && MaxTurnsCount < levelImages.Length) ? levelImages.Length : MaxTurnsCount;
        
        info = "Number of cards are required to be selected to make Match?";
    }
#endif
    
    [SerializeField][Range(2,4)] private int requiredCardPerMatchCount = 2;
    [field: SerializeField] public int MaxTurnsCount { get; private set;}
    [field: SerializeField] public int ScorePerMatch { get; private set;}
    [field: SerializeField] public Sprite[] levelImages { get; private set;}
    
    public int GetRequiredMatchesCount => (requiredCardPerMatchCount < 2) ? 2 : requiredCardPerMatchCount;//Just precaution😅
}
