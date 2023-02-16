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

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestManager manager = player.GetComponentInParent<QuestManager>();
        if (manager.hasQuest == true && manager.objectives.Length > 0)
        {
            if (manager.objectives[manager.currentStep] == questName)
            {
                if (other.gameObject.layer == 7)
                    manager.currentStep += 1;
                else if (needsItem && other.gameObject.name == itemNeeded)
                {
                    manager.currentStep += 1;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
