using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemScript : MonoBehaviour
{
    private Vector3 originalLocation;
    // Start is called before the first frame update
    void Start()
    {
        originalLocation = this.transform.position;
        if (PlayerPrefs.GetString("questItems").Contains(this.gameObject.name))
        {
            Destroy(this.gameObject);
        }
    }

    public void destroyItem()
    {
        if(!PlayerPrefs.GetString("questItems").Contains(this.gameObject.name))
            PlayerPrefs.SetString("questItems", PlayerPrefs.GetString("questItems") + " " + this.gameObject.name);

        Destroy(this.gameObject);
    }

    public void resetLocation()
    {
        this.transform.position = originalLocation;
    }
}
