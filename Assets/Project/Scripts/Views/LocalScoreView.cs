using UnityEngine;
using TMPro;

public class LocalScoreView : MonoBehaviour
{
    [SerializeField] private string prefix = "Score : ";
    [SerializeField] private TextMeshProUGUI textMatchFounded;
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            GameController.Instance.OnLocalScoreValueChanged += ShowUI;
            isSubscribed = true;
        }

        ShowUI(GameController.Instance.LocalScore);
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            GameController.Instance.OnLocalScoreValueChanged -= ShowUI;
            isSubscribed = false;
        }
    }
    
    private void ShowUI(int value)
    {
        textMatchFounded.text = $"{prefix}{value}";
        GameManager.Instance.Log("UI >> gameplay >> matchCountUI Updated: " + value);
    }

}
