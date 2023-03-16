using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float speed = 120;
    float gap;

    public AudioSource source;

    GameObject player;
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
        player = GameObject.Find("Toast");

        gap = Random.Range(300, 400);
        float size1 = Random.Range(gap, 1060);
        float size2 = -(1060-size1-50) - gap;
        this.transform.GetChild(0).localPosition = new Vector3(this.transform.GetChild(0).localPosition.x, size2, 0);

        this.transform.GetChild(1).localPosition = new Vector3(this.transform.GetChild(1).localPosition.x, size1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y);

        if(this.transform.position.x < -600)
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Toast")
        {
            player.GetComponent<BirdControl>().score += 1;
            source.Play();
        }
            
    }

}
