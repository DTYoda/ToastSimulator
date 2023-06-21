using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arcade : MonoBehaviour
{
    GameObject manager;
    SceneManage sceneManage;
    public Text interactText;
    bool isInRange;

    public bool flappyToast;
    public bool whackAMole;

    private void Start()
    {
        manager = GameObject.Find("SCENEMANAGER");
        if(manager != null)
        {
            sceneManage = GameObject.Find("SCENEMANAGER").GetComponent<SceneManage>();
        }
    }
    private void Update()
    {
        if(isInRange && sceneManage != null)
        {
            interactText.gameObject.SetActive(true);
            if(flappyToast)
                interactText.text = "Press F to play Flappy Toast!";
            else if(whackAMole)
                interactText.text = "Press F to play Whack-A-Mole!";
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(flappyToast)
                    sceneManage.FlappyBird(false, 0);
                if(whackAMole)
                    sceneManage.FlappyBird(false, 0);
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
