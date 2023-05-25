using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    public GameObject moleObject;

    public Vector2 topLeft;
    public Vector2 bottomRight;

    public GameObject topLeftObject;
    public GameObject bottomRightObject;
    void Start()
    {
        InvokeRepeating("SpawnMole", 2f, 2f);

        
    }

    // Update is called once per frame
    void Update()
    {
        topLeft = topLeftObject.transform.position;
        bottomRight = bottomRightObject.transform.position;
    }

    public void SpawnMole()
    {
        Instantiate(moleObject, new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y), 0), moleObject.transform.rotation, this.gameObject.transform);
    }
}
