using UnityEngine;
using TMPro;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTotalScore;
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            ScoreManager.Instance.OnScoreValueChanged += ShowSettingsUI;
            isSubscribed = true;
        }

        ShowSettingsUI(ScoreManager.Instance.GetTotalScores());
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            ScoreManager.Instance.OnScoreValueChanged -= ShowSettingsUI;
            isSubscribed = false;
        }
    }
    
    private void ShowSettingsUI(long value)
    {
        textTotalScore.text = value.ToString();
        GameManager.Instance.Log("UI >> Mainmenu >> TotalScores Updated: " + value);
    }
}
