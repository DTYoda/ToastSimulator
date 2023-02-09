using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string[] objectives;
    public string currentQuest;

    public Text questText;
    public GameObject questBox;

    public GameObject expandedQuestBox;
    public GameObject questImage;

    public int currentStep;
    public bool hasQuest = false;
    public int acceptedQuest = 0;

    public Animator questAnim;
    public AnimationClip expandClip;
    public Text expandedQuestTitle;
    public Text expandedQuestAuthor;
    public Text expandedQuestList;

    private bool isExpanded;
    private bool canExpand = true;

    private void Start()
    {    }

    private void Update()
    {
        if (hasQuest && currentStep < objectives.Length)
        {
            questText.text = currentQuest + ": " + objectives[currentStep];
            questBox.SetActive(true);

            expandedQuestTitle.text = currentQuest;

            if(expandedQuestList.text == "")
            {
                for (int i = 1; i <= objectives.Length; i++)
                {
                    expandedQuestList.text += i + ". " + objectives[i - 1] + "\n";
                }
            }
        }
        else
        {
            questText.text = "";
            questBox.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Q) && canExpand)
        {
            if (!isExpanded)
                StartCoroutine("expandAnim");
            else
                StartCoroutine("unexpandAnim");

        }

        if(currentStep == objectives.Length)
        {
            hasQuest = false;
            acceptedQuest = 0;
            currentStep = 0;
            currentQuest = "";
        }
    }

    public void Accept()
    {
        acceptedQuest = 1;
    }
    public void Decline()
    {
        acceptedQuest = -1;
    }

    IEnumerator expandAnim()
    {
        canExpand = false;
        questAnim.SetTrigger("expand");
        questText.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(expandClip.length);
        questImage.SetActive(false);
        expandedQuestBox.SetActive(true);
        isExpanded = true;
        canExpand = true;
    }

    IEnumerator unexpandAnim()
    {
        canExpand = false;
        questAnim.SetTrigger("unexpand");
        questImage.SetActive(true);
        expandedQuestBox.SetActive(false);
        yield return new WaitForSecondsRealtime(expandClip.length);
        questText.gameObject.SetActive(true);
        isExpanded = false;
        canExpand = true;
    }
}
