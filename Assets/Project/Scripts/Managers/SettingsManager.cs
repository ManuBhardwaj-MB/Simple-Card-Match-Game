using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public Action<int> OnDifficultySettingsChanged;
    public Action<bool> OnSfxSettingsChanged;

    private const int maxDifficultySettings = 4;
    private const string saveSfxSettingsKey = "SfxSaveKey";
    private const string saveDifficultySettingsKey = "DifficultySaveKey";
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int GetDifficultySettings() => PlayerPrefs.GetInt(saveDifficultySettingsKey, 0);
    public bool GetSfxSettings() => GetBoolFromInt(PlayerPrefs.GetInt(saveSfxSettingsKey, GetIntFromBool(true)));
   
    public void SetDifficultySettings(int value)
    {
        if (value < 0) value = 0;
        if (value > maxDifficultySettings) value = maxDifficultySettings;
        
        PlayerPrefs.SetInt(saveDifficultySettingsKey, value);
        
        OnDifficultySettingsChanged?.Invoke(value);
    }

    public void SetSfxOn(bool isOn)
    {
        PlayerPrefs.SetInt(saveSfxSettingsKey, GetIntFromBool(isOn));
        OnSfxSettingsChanged?.Invoke(isOn);
    }

    private int GetIntFromBool(bool value) => value ? 1 : 0;
    private bool GetBoolFromInt(int value) => value > 0;
}
