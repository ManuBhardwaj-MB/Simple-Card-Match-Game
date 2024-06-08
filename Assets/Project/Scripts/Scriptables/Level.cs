using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Level", menuName = "Configs/Levels/New-Level", order = 0)]
public class Level : ScriptableObject
{
    
#if UNITY_EDITOR
    [TextArea(1,1)] public string info = "Number of cards are required to be selected to make Match?";
    private void OnValidate()
    {
        requiredCardPerMatchCount = (requiredCardPerMatchCount < 2) ? 2 : requiredCardPerMatchCount;
        info = "Number of cards are required to be selected to make Match?";
    }
#endif
    
    [SerializeField][Range(2,4)] private int requiredCardPerMatchCount = 2;
    [field: SerializeField] public Sprite[] levelImages { get; private set;}
    
    public int GetRequiredMatchesCount => (requiredCardPerMatchCount < 2) ? 2 : requiredCardPerMatchCount;//Just precaution😅
}
