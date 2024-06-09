using UnityEngine;
using UnityEngine.UI;

public class SfxSettingsView : MonoBehaviour
{
    [SerializeField] private Color onLightColor;
    [SerializeField] private Color offLightColor;
    [SerializeField] private Image activeIndicatorImage;
    
    private bool isSubscribed;
    
    private void OnEnable()
    {
        if (!isSubscribed)
        {
            SettingsManager.Instance.OnSfxSettingsChanged += ShowSettingsUI;
            isSubscribed = true;
        }

        ShowSettingsUI(SettingsManager.Instance.GetSfxSettings());
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            SettingsManager.Instance.OnSfxSettingsChanged -= ShowSettingsUI;
            isSubscribed = false;
        }
    }
    
    private void ShowSettingsUI(bool value)
    {
        activeIndicatorImage.color = value ? onLightColor : offLightColor;
    }
    
    public void SetSettings()
    {
        SettingsManager.Instance.SetSfxOn(!SettingsManager.Instance.GetSfxSettings());
    }
}
