using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    Transform player;
    public int maxSize;
    public int minSize;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        if(PlayerPrefs.GetFloat("MinimapSize") == 0)
        {
            PlayerPrefs.SetFloat("MinimapSize", 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Camera>().orthographicSize = PlayerPrefs.GetFloat("MinimapSize");

        this.transform.position = new Vector3(player.position.x, player.position.y + 20, player.position.z);

        if(Input.GetKey(KeyCode.Minus) && PlayerPrefs.GetFloat("MinimapSize") > minSize)
        {
            PlayerPrefs.SetFloat("MinimapSize", PlayerPrefs.GetFloat("MinimapSize") - 5 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Equals) && PlayerPrefs.GetFloat("MinimapSize") < maxSize)
        {
            PlayerPrefs.SetFloat("MinimapSize", PlayerPrefs.GetFloat("MinimapSize") + 5 * Time.deltaTime);
        }

    }
}
