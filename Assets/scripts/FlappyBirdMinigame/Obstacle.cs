using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float speed = 70;
    float gap;

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Toast");

        gap = Random.Range(300, 500);
        float size1 = 1060 - gap - Random.Range(0, 1060 - gap);
        float size2 = 1060 - gap - size1;
        this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, size1);
        this.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(100, size1);
        this.transform.GetChild(0).GetComponent<BoxCollider2D>().offset = new Vector2(0, -size1 / 2);

        this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(100, size2);
        this.transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(100, size2);
        this.transform.GetChild(1).GetComponent<BoxCollider2D>().offset = new Vector2(0, size2 / 2);
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
            player.GetComponent<BirdControl>().score += 1;
    }
}
