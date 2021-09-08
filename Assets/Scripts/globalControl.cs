using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalControl : MonoBehaviour
{
    public static globalControl Instance;
    public int currentWave;
    void Awake()
    {
        
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
}
