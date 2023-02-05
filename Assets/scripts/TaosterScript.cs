using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaosterScript : MonoBehaviour
{
    private GameObject player;
    public Camera mainCamera;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if(player.GetComponent<ItemPickup>().previousHit != null)
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, 3, 11) && player.GetComponent<ItemPickup>().previousHit.name == "bread" && Input.GetKeyDown(KeyCode.F))
            {
                player.GetComponent<ItemPickup>().previousHit.transform.GetChild(0).gameObject.SetActive(true);
                player.GetComponent<ItemPickup>().previousHit.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        
    }


}
