using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainTheme;
    public AudioClip questTheme;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {

        QuestManager manager = player.GetComponent<QuestManager>();

        if(PlayerPrefs.GetInt("musicToggle") == 0)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }

        if (manager.hasQuest == true)
        {
            audioSource.clip = questTheme;
        }
        else
        {
            audioSource.clip = mainTheme;
        }

        if (audioSource.isPlaying == false)
            audioSource.Play();
    }
}
