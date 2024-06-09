using UnityEngine;
using UnityEngine.UI;

public class DifficultySettingsView : MonoBehaviour
{
    [SerializeField] private Color onLightColor;
    [SerializeField] private Color offLightColor;
    [SerializeField] private Image[] difficultyIndicators;
    
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            SettingsManager.Instance.OnDifficultySettingsChanged += ShowSettingsUI;
            isSubscribed = true;
        }

        ShowSettingsUI(SettingsManager.Instance.GetDifficultySettings());
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            SettingsManager.Instance.OnDifficultySettingsChanged -= ShowSettingsUI;
            isSubscribed = false;
        }
    }

    private void ShowSettingsUI(int value)
    {
        for (var i = 0; i < difficultyIndicators.Length; i++)
        {
            var property = difficultyIndicators[i];
            property.color = (value.Equals(i)) ? onLightColor : offLightColor;
        }
    }
    
    public void SetSettings(int value)
    {
        SettingsManager.Instance.SetDifficultySettings(value);
    }
}
