using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaosterScript : MonoBehaviour
{
    private GameObject player;
    public Camera mainCamera;
    public LayerMask mask;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if(player.GetComponent<ItemPickup>().hit.transform != null)
        {
            RaycastHit hit;
            Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 3, mask);   
            if(hit.transform != null)
            {
                if (hit.transform.name == this.gameObject.name && player.GetComponent<ItemPickup>().hit.transform.name == "bread" && Input.GetKeyDown(KeyCode.F))
                {
                    player.GetComponent<ItemPickup>().previousHit.transform.GetChild(0).gameObject.SetActive(true);
                    player.GetComponent<ItemPickup>().previousHit.transform.GetChild(1).gameObject.SetActive(false);
                }
            }   
        }
        
    }


}
