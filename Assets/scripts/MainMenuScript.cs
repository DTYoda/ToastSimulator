using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject nonPauseMenu;
    public Animator anim;
    public AnimationClip animClip;
    private GameObject player;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        MainMenu();
    }
    public void MainMenu()
    {
        player = GameObject.Find("Player");
        startMenu.SetActive(true);
        player.transform.eulerAngles = new Vector3(0, 180, 0);
        player.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = false;
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
}
