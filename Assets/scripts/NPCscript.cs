using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour
{
    //INDEX 0 MUST BE DIALOG FOR RECIEVING QUEST
    public string[] dialog;
    public string[] questObjectives;
    public int questXP;
    public bool hasQuest;
    public string npcName;
    public string questName;

    private GameObject player;
    public Rigidbody body;

    private bool isSpeaking;
    private bool isInside;
    private bool completeQuest;
    private bool acceptedQuest;

    private int speakText;
    public Text interactText;
    public Text[] acceptedQuestTexts;
    public GameObject questAcceptObject;

    private bool isLooking;

    Vector3 lookPosition;
    void Start()
    {
        lookPosition = this.transform.position;

        body = this.gameObject.GetComponent<Rigidbody>();
        interactText.gameObject.SetActive(false);
        player = GameObject.Find("Player");

        if(PlayerPrefs.GetString("quests").Contains(questName))
        {
            completeQuest = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //makes the NPC look towards the player if they're in the same room     
        RaycastHit hitPlayer;
        Physics.Raycast(this.transform.position, (player.transform.position - this.transform.position).normalized, out hitPlayer);
        if(hitPlayer.transform.gameObject.CompareTag("Player"))
        {
            lookPosition = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        }
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        //If the player is speaking with the NPC, call the AskForQuest function
        if (isSpeaking)
        {
            AskForQuest();
        }

        //If the Player is within the NPC's radius
        if (isInside)
        {
            //if you're not already speaking with the NPC, set text to pre-speaking text
            if (!isSpeaking)
            {
                interactText.text = "Press F to speak with " + npcName;
            }
            interactText.gameObject.SetActive(true);
            //if F key is pressed, initiate speaking
            if (Input.GetKeyDown(KeyCode.F))
            {
                speakText = Random.Range(1, dialog.Length);
                isSpeaking = true;
            }
        }

        //If this NPC has a quest and it has not been accepted or completed, show a quest icon above its head
        if (hasQuest && !acceptedQuest && !completeQuest)
        {
            QuestIcon();
        }
        else
        {
            questIcon.SetActive(false);
        }

        //constantly check if this NPC's quest has been completed
        checkForCompletion();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            isSpeaking = false;
            isInside = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            interactText.gameObject.SetActive(false);
            interactText.text = "";
            isSpeaking = false;
            isInside = false;
        }

    }

    private void AskForQuest()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        if (hasQuest && !acceptedQuest && !completeQuest && manager.hasQuest == false)
        {
            speakText = 0;
            interactText.text = dialog[speakText];
            questAcceptObject.SetActive(true);
            acceptedQuestTexts[0].text = "Accept Quest: " + questName + "?";
            acceptedQuestTexts[1].text = "This Quest Has " + questObjectives.Length + " steps";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            if (manager.acceptedQuest == 1)
            {
                manager.hasQuest = true;
                manager.currentStep = 0;
                manager.objectives = questObjectives;
                manager.currentQuest = questName;
                manager.questXP = questXP;
                questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;
                Cursor.lockState = CursorLockMode.Locked;
                acceptedQuest = true;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            else if (manager.acceptedQuest == -1)
            {
                isSpeaking = false;
                questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }


        }
        else
        {
            interactText.text = dialog[speakText];
        }
    }

    public GameObject questIcon;
    bool i = false;
    private void QuestIcon()
    {

        if (questIcon.transform.localPosition.y > 1.6)
        {
            i = true;
        }
        else if (questIcon.transform.localPosition.y < 1.3)
        {
            i = false;
        }

        if (i)
        {
            questIcon.transform.position -= new Vector3(0, 0.5f * Time.deltaTime, 0);
        }
        else
        {
            questIcon.transform.position += new Vector3(0, 0.5f * Time.deltaTime, 0);
        }

    }

    private void checkForCompletion()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        if(acceptedQuest && manager.currentQuest != questName && completeQuest == false)
        {
            completeQuest = true;
            PlayerPrefs.SetString("quests", PlayerPrefs.GetString("quests") + " " + questName);
        }
    }
}
