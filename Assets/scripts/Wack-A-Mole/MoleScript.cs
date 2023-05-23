using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
{
    EventManagerMole manager;
    void Start()
    {
        manager = GameObject.Find("EventSystem").GetComponent<EventManagerMole>();
    }

    private void Awake()
    {
        StartCoroutine("SelfDustruct");
    }
    public void MoleClick()
    {
        manager.score++;
        Destroy(this.gameObject);
    }

    IEnumerator SelfDustruct()
    {
        yield return new WaitForSeconds(Random.Range(300, 2000) / 1000.0f);
        Destroy(this.gameObject);
    }
}
