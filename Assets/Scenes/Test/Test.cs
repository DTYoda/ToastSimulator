using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRB;
    public GameObject planet;

    private void Update()
    {
        playerRB.AddForce((planet.transform.position - player.transform.position).normalized * 9.8f);
    }


}
