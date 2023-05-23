using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventManagerMole : MonoBehaviour
{
    public GameObject player;
    public GameObject game;
    public Text highscoreText;
    public Text scoreText;
    public GameObject sceneManger;
    public GameObject questCompletion;

    public AudioSource source;
    public AnimationClip clip;
    bool completionSound = false;

    public int score;

    bool quest;
    int required;
    void Start()
    {
        if (PlayerPrefs.GetInt("musicToggle") == 0)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }
        

        sceneManger = GameObject.Find("SCENEMANAGER");
        if (PlayerPrefs.GetInt("effectsToggle") == 0)
        {
            source.volume = 0;
        }
        else
        {
            source.volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }
        if (sceneManger != null)
        {
            quest = sceneManger.GetComponent<SceneManage>().flappyBirdQuest;
            required = sceneManger.GetComponent<SceneManage>().flappyBirdRequired;
        }
    }

    // Update is called once per frame
    void Update()
    {
        highscoreText.text = "High Score: " + PlayerPrefs.GetInt("MoleScore").ToString();
        scoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("MoleScore"))
        {
            PlayerPrefs.SetInt("MoleScore", score);
        }

        if(quest && required <= player.GetComponent<BirdControl>().score)
        {
            sceneManger.GetComponent<SceneManage>().completeFlappyBird = true;
            if(!completionSound)
            {
                StartCoroutine("QuestCompletion");
            }
        }
    }

    public void startGame()
    {
        game.SetActive(true);
        score = 0;
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

    IEnumerator QuestCompletion()
    {
        completionSound = true;
        questCompletion.SetActive(true);
        questCompletion.GetComponent<Animator>().SetTrigger("play");
        source.Play();
        yield return new WaitForSecondsRealtime(clip.length);
        questCompletion.SetActive(false);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(30);
    }
}
