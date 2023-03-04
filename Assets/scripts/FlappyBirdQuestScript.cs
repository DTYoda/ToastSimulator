using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdQuestScript : MonoBehaviour
{
    private GameObject player;
    private GameObject sceneManage;
    public string[] newDialouge;
    private void Start()
    {
        player = GameObject.Find("Player");
        sceneManage = GameObject.Find("SCENEMANAGER");
    }
    private void Update()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        NPCscript script = this.GetComponent<NPCscript>();
        if (manager.currentQuest == "Missing Child" && (manager.currentStep == 1 || manager.currentStep == 2))
        {
            script.speakText = 0;
            if(manager.currentStep == 1)
                manager.currentStep += 1;
            if (script.isSpeaking)
            {
                script.questAcceptObject.SetActive(true);
                script.acceptedQuestTexts[0].text = "Help Jimmy beat this level?";
                script.acceptedQuestTexts[1].text = "You will have to get a score of 10 in Flappy Toast";
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                if (manager.acceptedQuest == 1)
                {
                    script.questAcceptObject.SetActive(false);
                    manager.acceptedQuest = 0;
                    if (sceneManage != null)
                    {
                        sceneManage.GetComponent<SceneManage>().FlappyBird(true, 10);
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

        if(PlayerPrefs.GetString("quests").Contains("Missing Child"))
        {
            this.transform.localPosition = new Vector3(28.191f, -0.33f, -48.914f);
            script.dialog = newDialouge;
        }
    }
}
