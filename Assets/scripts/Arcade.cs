using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arcade : MonoBehaviour
{
    SceneManage manager;
    public Text interactText;
    bool isInRange;

    private void Start()
    {
        manager = GameObject.Find("SCENEMANAGER").GetComponent<SceneManage>();
    }
    private void Update()
    {
        if(isInRange)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press F to play Flappy Toast";
            if(Input.GetKeyDown(KeyCode.F))
            {
                manager.FlappyBird(false, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== 7)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            interactText.gameObject.SetActive(false);
            interactText.text = "";
            isInRange = false;
        }
    }
}
