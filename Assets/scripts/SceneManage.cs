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

    public bool moleQuest;
    public int moleRequired;
    public bool completeMole;
    public int moleXP;

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

    public void WhackAMole(bool i, int r)
    {
        moleQuest = i;
        moleRequired = r;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;

        if (player != null)
        {
            playerLocation = player.transform.position;
        }

        SceneManager.LoadScene("WackAMoleMinigame");
    }

    public void Return()
    {
        returned = true;

        if (flappyBirdQuest)
        {
            if (completeFlappyBird)
            {
                PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + 10);
                PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + 5);
                PlayerPrefs.SetString("quests", PlayerPrefs.GetString("quests") + " Missing Child");
            }
        }
        else if (moleQuest && completeMole)
        {
            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + 10);
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + 5);
            PlayerPrefs.SetString("quests", PlayerPrefs.GetString("quests") + " Whack-A-Mole");
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
            }
        }
        GameObject.Find("MainMenu").GetComponent<MainMenuScript>().StartGameNoAnim(playerLocation);
    }
}
