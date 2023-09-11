using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity;
    float jump = 280;
    public Text scoreText;

    public AudioClip[] sounds;
    public AudioSource source;


    public GameObject gameOverMenu;
    public int score;
    void Start()
    {
        if (PlayerPrefs.GetInt("effectsToggle") == 0)
        {
            source.volume = 0;
        }
        else
        {
            source.volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            source.clip = sounds[1];
            source.Play();
            rb.velocity = Vector2.up * jump;
        }

        scoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("FlappyScore"))
        {
            PlayerPrefs.SetInt("FlappyScore", score);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Top" || collision.gameObject.name == "Bottom")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        source.clip = sounds[0];
        source.Play();
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }    
}
