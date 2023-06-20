using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackAMoleQuestScript : MonoBehaviour
{
    private GameObject player;
    private GameObject sceneManage;
    public string[] newDialouge;
    private bool uiActive;
    private void Start()
    {
        player = GameObject.Find("Player");
        sceneManage = GameObject.Find("SCENEMANAGER");
    }
    private void Update()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        NPCscript script = this.GetComponent<NPCscript>();
        if (manager.currentQuest == "Whack-A-Mole" && (manager.currentStep == 2 || manager.currentStep == 3))
        {
            script.speakText = 0;
            if(manager.currentStep == 1)
                manager.currentStep += 1;
            if (script.isSpeaking)
            {
                script.questAcceptObject.SetActive(true);
                script.acceptedQuestTexts[0].text = "Whack the moles in Buff Jack's yard?";
                script.acceptedQuestTexts[1].text = "You will have to Whack 24 Moles in 30 seconds.";
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                if (manager.acceptedQuest == 1)
                {
                    script.questAcceptObject.SetActive(false);
                    manager.acceptedQuest = 0;
                    if (sceneManage != null)
                    {
                        sceneManage.GetComponent<SceneManage>().WhackAMole(true, 24);
                    }
                    else
                    {
                        script.isSpeaking = false;
                        script.questAcceptObject.SetActive(false);
                        manager.acceptedQuest = 0;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Time.timeScale = 1;
                    }
                }
                else if (manager.acceptedQuest == -1)
                {
                    script.isSpeaking = false;
                    script.questAcceptObject.SetActive(false);
                    manager.acceptedQuest = 0;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                }
            }
        }

        if(PlayerPrefs.GetString("quests").Contains("Whack-A-Mole"))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        NPCscript script = this.GetComponent<NPCscript>();
        if (other.CompareTag("Player") && manager.currentQuest == "Whack-A-Mole" && (manager.currentStep == 1 || manager.currentStep == 2))
        {
            uiActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            uiActive = false;
        }
    }
}
