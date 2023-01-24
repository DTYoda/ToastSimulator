using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject nonPauseMenu;
    private bool isPaused = false;
    public Animator anim;
    public AnimationClip pause;
    public AnimationClip unpause;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                StartCoroutine("unPause");
            }
            else
            {
                StartCoroutine("Pause");
            }
        }
    }
    IEnumerator Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        nonPauseMenu.SetActive(false);
        anim.SetTrigger("Pause");
        yield return new WaitForSecondsRealtime(pause.length);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    IEnumerator unPause()
    {
        isPaused = false;
        anim.SetTrigger("Unpause");
        yield return new WaitForSecondsRealtime(unpause.length);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        nonPauseMenu.SetActive(true);
    }

    public void unpauseButton()
    {
        StartCoroutine("unPause");
    }
}
