using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle;
    void Start()
    {
        InvokeRepeating("Spawn", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Instantiate(obstacle, this.gameObject.transform);
    }
}
