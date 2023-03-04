using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public bool flappyBirdQuest;
    public int flappyBirdRequired;
    public bool completeFlappyBird;

    public Vector3 playerLocation;
    public GameObject player;

    public bool returned = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Level1");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            FlappyBird(false, 0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Return();
        }
    }

    public void FlappyBird(bool i, int r)
    {
        flappyBirdQuest = i;
        flappyBirdRequired = r;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;

        if (player != null)
        {
            playerLocation = player.transform.position;
        }

        SceneManager.LoadScene("FlappyBirdMinigame");
    }

    public void Return()
    {
        returned = true;
        SceneManager.LoadScene("Level1");
        
    }
    public void Return2()
    {
        returned = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        GameObject.Find("MainMenu").SetActive(false);

        if (player != null)
        {
            player.transform.position = playerLocation;
        }
        else
        {
            Debug.Log("failed");
        }
    }
}
