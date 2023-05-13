using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    //must be the same as quest step
    public string questName;
    public bool needsItem = false;
    public string itemNeeded = "";
    private GameObject player;
    public bool hasRadius;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player != null)
        {
            QuestManager manager = player.GetComponent<QuestManager>();
            if (manager.hasQuest == true && manager.objectives.Length > manager.currentStep && manager.objectives[manager.currentStep] == questName)
            {
                if (other.gameObject.layer == 7 && !needsItem)
                    manager.currentStep += 1;
                else if (needsItem && other.gameObject.name == itemNeeded)
                {
                    manager.currentStep += 1;
                    other.gameObject.GetComponent<QuestItemScript>().destroyItem();
                }
            }
        }
        
    }

    private void Update()
    {
        if(player != null && hasRadius)
        {
            QuestManager manager = player.GetComponent<QuestManager>();
            if (manager.hasQuest == true && manager.objectives.Length > manager.currentStep && manager.objectives[manager.currentStep] == questName)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
