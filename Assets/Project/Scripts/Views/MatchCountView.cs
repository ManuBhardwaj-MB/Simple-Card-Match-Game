using UnityEngine;
using TMPro;

public class MatchCountView : MonoBehaviour
{
    [SerializeField] private string prefix = "Matches : ";
    [SerializeField] private TextMeshProUGUI textMatchFounded;
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            GameController.Instance.OnMatchFoundValueChanged += ShowUI;
            isSubscribed = true;
        }

        ShowUI(GameController.Instance.MatchFoundedCount);
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            GameController.Instance.OnMatchFoundValueChanged -= ShowUI;
            isSubscribed = false;
        }
    }
    
    private void ShowUI(int value)
    {
        textMatchFounded.text = $"{prefix}{value}";
        GameManager.Instance.Log("UI >> gameplay >> matchCountUI Updated: " + value);
    }
    
}
