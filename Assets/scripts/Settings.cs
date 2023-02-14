using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Toggle musicToggle;
    public Toggle effectsToggle;

    public Text volumeText;
    public Text sensitivityText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("sens") == 0)
        {
            PlayerPrefs.SetInt("sens", 50);
        }

        volumeSlider.value = PlayerPrefs.GetInt("volume");
        sensitivitySlider.value = PlayerPrefs.GetInt("sens");

        if (PlayerPrefs.GetInt("musicToggle") == 0)
        {
            musicToggle.isOn = false;
        }
        else
        {
            musicToggle.isOn = true;
        }
    }

    public void volumeChange()
    {
        PlayerPrefs.SetInt("volume", (int) volumeSlider.value);
        volumeText.text = volumeSlider.value.ToString();
    }

    public void sensChange()
    {
        PlayerPrefs.SetInt("sens", (int) sensitivitySlider.value);
        sensitivityText.text = sensitivitySlider.value.ToString();
    }

    public void musicToggleChange()
    {
        if(musicToggle.isOn == true)
        {
            PlayerPrefs.SetInt("musicToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("musicToggle", 0);
        }
    }
}
