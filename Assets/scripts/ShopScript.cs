using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    GameObject player;
    public GameObject shopMenu;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        NPCscript script = this.GetComponent<NPCscript>();
        QuestManager manager = player.GetComponent<QuestManager>();
        if (script.isSpeaking)
        {
            script.questAcceptObject.SetActive(true);
            script.acceptedQuestTexts[0].text = "Enter Shop?";
            script.acceptedQuestTexts[1].text = "";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            if (manager.acceptedQuest == 1)
            {
                script.questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;
                script.isSpeaking = false;
                script.questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;  
                Time.timeScale = 1;
                shopMenu.SetActive(true);
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

    public void ShopExit()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        shopMenu.SetActive(false);
    }
}
