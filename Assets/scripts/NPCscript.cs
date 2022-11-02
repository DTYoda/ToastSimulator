using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour
{
    public string[] dialog;
    public GameObject player;
    public Rigidbody body;

    private bool isSpeaking;
    private int speakText;
    private bool isInside;

    public string npcName;

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
            interactText.text = dialog[speakText];
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
                speakText = Random.Range(0, dialog.Length);
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
            isInside = false;
        }

    }
}
