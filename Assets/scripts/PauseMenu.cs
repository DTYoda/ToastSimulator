using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject nonPauseMenu;
    private bool isPaused = false;
    public bool canPause = true;
    public Animator anim;
    public AnimationClip pause;
    public AnimationClip unpause;

    private GameObject player;
    public Camera mainCamera;
    private float x;
    private float y;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                StartCoroutine("unPause");
            }
            else if(Time.timeScale == 1)
            {
                StartCoroutine("Pause");
            }
        }
    }
    IEnumerator Pause()
    {
        x = mainCamera.transform.localEulerAngles.x;
        y = player.transform.localEulerAngles.y;
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
        player.GetComponent<CameraController>().resetXY(x, y);
    }

    public void unpauseButton()
    {
        StartCoroutine("unPause");
    }

    public void UnpauseVariable()
    {
        SceneManager.LoadScene("Level1");
    }
}
