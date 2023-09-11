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
    public float questCash;
    public bool hasQuest;
    public string npcName;
    public string questName;
    public string questDescription;

    private GameObject player;
    public Rigidbody body;

    public bool isSpeaking;
    private bool isInside;
    private bool completeQuest = false;
    private bool acceptedQuest = false;
    private int timesTalkedTo = 0;

    public int speakText;
    public Text interactText;
    public GameObject speechText;
    public Text[] acceptedQuestTexts;
    public GameObject questAcceptObject;

    private bool isLooking;

    bool randomLook = true;
    Quaternion rotation;

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
        if(hitPlayer.transform != null && hitPlayer.transform.gameObject.CompareTag("Player") && Vector3.Distance(hitPlayer.transform.position, this.transform.position) < 10)
        {
            lookPosition = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            rotation = Quaternion.LookRotation(lookPosition);
        }
        else if(randomLook)
        {
            StartCoroutine("RandomLook");
        }
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
                if(canSpeak)
                {
                    speakText = (timesTalkedTo % (dialog.Length - 1)) + 1;
                    timesTalkedTo++;
                }
                    
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
        RaycastHit hitPlayer;
        Physics.Raycast(this.transform.position, (player.transform.position - this.transform.position).normalized, out hitPlayer);
        if (other.gameObject.layer == 7 && hitPlayer.transform != null && hitPlayer.transform.gameObject.CompareTag("Player"))
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
            speechText.gameObject.SetActive(false);
            StopAllCoroutines();
            canSpeak = true;
            isSpeaking = false;
            isInside = false;
        }

    }

    bool canSpeak = true;
    private void AskForQuest()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        if (hasQuest && !acceptedQuest && !completeQuest && manager.hasQuest == false)
        {
            speakText = 0;
            if (canSpeak)
            {
                StartCoroutine("speakTimed");
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    StopAllCoroutines();
                    canSpeak = true;
                }
            }
            //pull up UI to ask for quest
            questAcceptObject.SetActive(true);
            acceptedQuestTexts[0].text = "Accept quest: " + questName + "?";
            acceptedQuestTexts[1].text = questDescription;
            //give player access to cursor and stop game speed
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

            //if quest was accepted
            if (manager.acceptedQuest == 1)
            {
                //set all needed variables inside quest manager object
                manager.hasQuest = true;
                manager.currentStep = 0;
                manager.objectives = questObjectives;
                manager.currentQuest = questName;
                manager.questXP = questXP;
                manager.questCash = questCash;
                questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;
                manager.questDesc = questDescription;
                Cursor.lockState = CursorLockMode.Locked;
                acceptedQuest = true;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            //if quest was declined
            else if (manager.acceptedQuest == -1)
            {
                isSpeaking = false;
                questAcceptObject.SetActive(false);
                manager.acceptedQuest = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                manager.acceptedQuest = 0;
            }


        }
        //if NPC doesn't have quest, start normal speech;
        else
        {
            if (canSpeak)
            {
                StartCoroutine("speakTimed");
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    StopAllCoroutines();
                    canSpeak = true;
                }
            }
        }
    }

    public GameObject questIcon;
    bool i = false;

    //move blue quest icon up and down
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

    //check if the NPC's quest has been completed
    private void checkForCompletion()
    {
        QuestManager manager = player.GetComponent<QuestManager>();
        if(acceptedQuest && manager.currentQuest != questName && completeQuest == false)
        {
            completeQuest = true;
            //adds the quest to PlayerPrefs, allowing for saved progress
            PlayerPrefs.SetString("quests", PlayerPrefs.GetString("quests") + " " + questName);
        }
    }

    //text "typing" animation
    IEnumerator speakTimed()
    {
        canSpeak = false;
        Text text = speechText.transform.GetChild(0).GetComponent<Text>();
        if (!(text.text == dialog[speakText] && speechText.activeSelf))
        {  
            text.text = "";
            speechText.gameObject.SetActive(true);
            for (int i = 0; i < dialog[speakText].Length; i++)
            {
                text.text += dialog[speakText][i];
                yield return new WaitForSecondsRealtime(0.03f);
            }
        }
        canSpeak = true;
    }

    //randomly look around if the player is not nearby
    IEnumerator RandomLook()
    {
        randomLook = false;
        rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        yield return new WaitForSeconds(Random.Range(2, 5));
        randomLook = true;
    }
}
