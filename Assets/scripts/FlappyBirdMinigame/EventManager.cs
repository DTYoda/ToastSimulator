using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventManager : MonoBehaviour
{
    public GameObject player;
    public GameObject game;
    public Text highscoreText;
    public GameObject sceneManger;

    bool quest;
    int required;
    void Start()
    {
        sceneManger = GameObject.Find("SCENEMANAGER");
        if(sceneManger != null)
        {
            quest = sceneManger.GetComponent<SceneManage>().flappyBirdQuest;
            required = sceneManger.GetComponent<SceneManage>().flappyBirdRequired;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        highscoreText.text = "High Score: " + PlayerPrefs.GetInt("FlappyScore").ToString();

        if(quest && required <= player.GetComponent<BirdControl>().score)
        {
            sceneManger.GetComponent<SceneManage>().completeFlappyBird = true;
        }
    }

    public void startGame()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        game.SetActive(true);
        player.SetActive(true);
        player.GetComponent<RectTransform>().localPosition = new Vector2(-750, 0);
        player.GetComponent<BirdControl>().score = 0;
        for(int i = 0; i < game.transform.Find("Obstacles").transform.childCount; i++)
        {
            Destroy(game.transform.Find("Obstacles").transform.GetChild(i).gameObject);
        }

        game.transform.Find("GameOver").gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        if(sceneManger != null)
        {
            sceneManger.GetComponent<SceneManage>().Return();
        }
    }
}