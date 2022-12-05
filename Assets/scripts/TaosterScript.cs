using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaosterScript : MonoBehaviour
{
    public GameObject questMarker1;
    public GameObject questMarker2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bread"))
        {
            Destroy(other.gameObject);
            questMarker1.SetActive(true);
            questMarker2.SetActive(true);

        }
    }
}
