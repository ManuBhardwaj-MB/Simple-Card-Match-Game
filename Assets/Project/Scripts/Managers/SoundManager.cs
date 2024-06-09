using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip gameWonSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip matchSound;
    [SerializeField] private AudioClip missMatchSound;
    [SerializeField] private AudioClip flipSound;
    
    [SerializeField] private List<AudioSource> audioSources;

    private void Awake() => Instance ??= this;

    public void PlayClickSound() => PlaySound(clickSound);
    public void PlayGameWonSound() => PlaySound(gameWonSound);
    public void PlayGameOverSound() => PlaySound(gameOverSound);
    public void PlayMatchSound() => PlaySound(matchSound);
    public void PlayMissMatchSound() => PlaySound(missMatchSound);
    public void PlayFlipSound() => PlaySound(flipSound);
    
    private void PlaySound(AudioClip clip)
    {
        if(SettingsManager.Instance.GetSfxSettings()) return;
        
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
                return; 
            }
        }
        audioSources[0].PlayOneShot(clip);
    }
    
}


