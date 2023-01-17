using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour
{
    //INDEX 0 MUST BE DIALOG FOR RECIEVING QUEST
    public string[] dialog;
    public string[] questObjectives;

    public GameObject player;
    public Rigidbody body;

    private bool isSpeaking;
    private bool isInside;
    public bool hasQuest;
    private bool completeQuest;
    private bool acceptedQuest;

    public string npcName;
    public string questName;

    private int speakText;
    public Text interactText;
    public Text[] acceptedQuestTexts;
    public GameObject questAcceptObject;

    private bool isLooking;
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
        interactText.gameObject.SetActive(false);

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        if (isSpeaking)
        {
            AskForQuest();

        }
        if (isInside)
        {
            if (!isSpeaking)
            {
                interactText.text = "Press F to speak with... " + npcName;
            }
            interactText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                speakText = Random.Range(1, dialog.Length);
                isSpeaking = true;
            }
        }
        if (hasQuest && !acceptedQuest && !completeQuest)
        {
            QuestIcon();
        }
        else
        {
            questIcon.SetActive(false);
        }

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
        if (hasQuest && !acceptedQuest && !completeQuest && player.GetComponent<QuestManager>().hasQuest == false)
        {
            speakText = 0;
            interactText.text = dialog[speakText];
            questAcceptObject.SetActive(true);
            acceptedQuestTexts[0].text = "Accept Quest: " + questName + "?";
            acceptedQuestTexts[1].text = "This Quest Has " + questObjectives.Length + " steps";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            if (player.GetComponent<QuestManager>().acceptedQuest == 1)
            {
                player.GetComponent<QuestManager>().hasQuest = true;
                player.GetComponent<QuestManager>().currentStep = 0;
                player.GetComponent<QuestManager>().objectives = questObjectives;
                player.GetComponent<QuestManager>().currentQuest = questName;
                questAcceptObject.SetActive(false);
                player.GetComponent<QuestManager>().acceptedQuest = 0;
                Cursor.lockState = CursorLockMode.Locked;
                acceptedQuest = true;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            else if (player.GetComponent<QuestManager>().acceptedQuest == -1)
            {
                isSpeaking = false;
                questAcceptObject.SetActive(false);
                player.GetComponent<QuestManager>().acceptedQuest = 0;
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
}
