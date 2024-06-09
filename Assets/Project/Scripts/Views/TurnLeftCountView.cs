using UnityEngine;
using TMPro;
public class TurnLeftCountView : MonoBehaviour
{
    [SerializeField] private string prefix = "Turns : ";
    [SerializeField] private TextMeshProUGUI textMatchFounded;
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            GameController.Instance.OnTurnLeftValueChanged += ShowUI;
            isSubscribed = true;
        }

        ShowUI(GameController.Instance.TurnLeft);
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            GameController.Instance.OnTurnLeftValueChanged -= ShowUI;
            isSubscribed = false;
        }
    }
    
    private void ShowUI(int value)
    {
        textMatchFounded.text = $"{prefix}{value}";
    }

}
