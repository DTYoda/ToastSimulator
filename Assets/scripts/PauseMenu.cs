using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject nonPauseMenu;
    private bool isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                unPause();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        nonPauseMenu.SetActive(false);
    }
    public void unPause()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        nonPauseMenu.SetActive(true);
    }
}
