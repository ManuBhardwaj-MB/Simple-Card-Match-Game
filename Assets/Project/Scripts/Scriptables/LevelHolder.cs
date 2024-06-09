using UnityEngine;

[CreateAssetMenu(fileName = "Level Holder", menuName = "Configs/Levels/New-Level-Holder", order = 0)]
public class LevelHolder : ScriptableObject
{
    [SerializeField] private Level[] levels;

    public Level GetLevel(int levelIndex)
    {
        if (levelIndex < levels.Length)
        {
            if (!levels.Equals(null))
            {
                return levels[levelIndex];
            }
        }

        return levels[0];
    }
}