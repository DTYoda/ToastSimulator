using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string[] objectives;
    public string currentQuest;

    public Text questText;

    public int currentStep;

    public bool hasQuest = false;

    private void Update()
    {
        if (hasQuest)
        {
            questText.text = currentQuest + ": " + objectives[currentStep];
        }
        else
        {
            questText.text = "";
        }    
    }
}
