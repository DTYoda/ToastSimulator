using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    //must be the same as quest step
    public string questName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            QuestManager manager = other.gameObject.GetComponentInParent<QuestManager>();
            if(manager.hasQuest == true)
            {
                if (manager.objectives[manager.currentStep] == questName)
                {
                    manager.currentStep += 1;
                }
            }

        }
    }
}
