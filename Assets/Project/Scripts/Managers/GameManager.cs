using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private bool debugingIsOn;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //Just Added here for temp purpose
    public void Log(string value)
    {
        if(!debugingIsOn) return;
        Debug.Log(value);
    }
}
