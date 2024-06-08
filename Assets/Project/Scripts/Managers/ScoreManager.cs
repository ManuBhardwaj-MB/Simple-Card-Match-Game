using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private const string saveTotalScoreKey = "TotalScoreSaveKey";
    public Action<long> OnScoreValueChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public long GetTotalScores()
    {
        if (long.TryParse(PlayerPrefs.GetString(saveTotalScoreKey, "0"), out long result))
        {
            return result;
        }
        return 0;
    }
    
    public void AddScoreToTotalScore(int value)
    {
        var newTotalScore = GetTotalScores() + value;
        PlayerPrefs.SetString(saveTotalScoreKey, newTotalScore.ToString());
        OnScoreValueChanged?.Invoke(newTotalScore);
    } 
}
