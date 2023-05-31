using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    public GameObject moleObject;
    public EventManagerMole manager;

    public Vector2 topLeft;
    public Vector2 bottomRight;

    public GameObject topLeftObject;
    public GameObject bottomRightObject;
    // Update is called once per frame
    void Update()
    {
        topLeft = topLeftObject.transform.position;
        bottomRight = bottomRightObject.transform.position;
    }
    IEnumerator SpawnMole()
    {
        if(manager.hasTimeLimit)
        {
            while (manager.timeLimit != 0)
            {
                yield return new WaitForSeconds(2.0f * ((manager.timeLimit + 3.0f) / manager.previousTime));
                Instantiate(moleObject, new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y), 0), moleObject.transform.rotation, this.gameObject.transform);
            }
        }
        else
        {
            yield return new WaitForSeconds(10 * Mathf.Log10(manager.timeLimit));
            Instantiate(moleObject, new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(bottomRight.y, topLeft.y), 0), moleObject.transform.rotation, this.gameObject.transform);
        }
        
    }
}
