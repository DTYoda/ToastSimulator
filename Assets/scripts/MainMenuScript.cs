using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject nonPauseMenu;
    public Animator anim;
    public AnimationClip animClip;
    public GameObject player;

    private void Awake()
    {
        Time.timeScale = 0;
        if(PlayerPrefs.GetInt("completeTutorial") == 0)
        {
            if (PlayerPrefs.GetInt("volume") == 0)
            {
                PlayerPrefs.SetInt("volume", 50);
            }
            if (PlayerPrefs.GetInt("sens") == 0)
            {
                PlayerPrefs.SetInt("sens", 50);
            }

            PlayerPrefs.SetInt("musicToggle", 1);
            PlayerPrefs.SetInt("effectsToggle", 1);
        }
    }

    private void Start()
    {
        MainMenu();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CameraController>().resetXY(0, 180);
        startMenu.SetActive(true);
        startMenu.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.eulerAngles = new Vector3(0, 180, 0);
        player.GetComponent<Animator>().enabled = true;
        player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Time.timeScale = 0;
    }

    public void startGame()
    {
        StartCoroutine("startGameAnim");
    }

    IEnumerator startGameAnim()
    {
        anim.SetTrigger("Start");
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(animClip.length);
        Time.timeScale = 1;
        startMenu.SetActive(false);
        nonPauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Animator>().enabled = false;
        player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CameraController>().resetXY(0, 180);
    }


    public void StartGameNoAnim(Vector3 location)
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
        startMenu.SetActive(false);
        nonPauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Animator>().enabled = false;
        player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CameraController>().resetXY(0, 180);
        player.transform.position = location;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
