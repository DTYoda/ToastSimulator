using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public bool flappyBirdQuest;
    public int flappyBirdRequired;
    public bool completeFlappyBird;
    public int flappyBirdXP;

    public Vector3 playerLocation = Vector3.zero;
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

        if (flappyBirdQuest)
        {
            if (completeFlappyBird)
            {
                PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + flappyBirdXP);
                PlayerPrefs.SetString("quests", PlayerPrefs.GetString("quests") + " Missing Child");
            }
            else
            {

            }
        }

            SceneManager.LoadScene("Level1");
        
    }

    public void Return2()
    {
        returned = false;
        if (player != null)
        {
            QuestManager manager = player.GetComponent<QuestManager>();
            if (flappyBirdQuest)
            {
                flappyBirdQuest = false;
                flappyBirdRequired = 0;
                if (completeFlappyBird)
                {
                    manager.CompleteQuestSound();
                }
                else
                {

                }
            }
        }
        GameObject.Find("MainMenu").GetComponent<MainMenuScript>().StartGameNoAnim(playerLocation);
    }
}
