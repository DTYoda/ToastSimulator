using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLoader : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    GameObject houseInsides;

    void Start()
    {
        player = GameObject.Find("Player");
        houseInsides = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) >= 50)
        {
            houseInsides.SetActive(false);
        }
        else
        {
            houseInsides.SetActive(true);
        }
    }
}
