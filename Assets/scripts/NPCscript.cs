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
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
        interactText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        if (isSpeaking)
        {
            if (hasQuest && !acceptedQuest && !completeQuest && player.GetComponent<QuestManager>().hasQuest == false)
            {
                player.GetComponent<QuestManager>().hasQuest = true;
                player.GetComponent<QuestManager>().currentStep = 0;
                player.GetComponent<QuestManager>().objectives = questObjectives;
                player.GetComponent<QuestManager>().currentQuest = questName;
                speakText = 0;
            }
            else
            {
                interactText.text = dialog[speakText];
            }

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
}
