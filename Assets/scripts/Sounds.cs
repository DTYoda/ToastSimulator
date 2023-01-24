using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    void Update()
    {
        if(PlayerPrefs.GetInt("musicToggle") == 0)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }
    }
}
