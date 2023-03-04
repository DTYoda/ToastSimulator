using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity;
    float jump = 220;
    public Text scoreText;


    public GameObject gameOverMenu;
    public int score;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
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
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }    
}
