using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string[] objectives;
    public string currentQuest;
    public int questXP = 0;
    public float questCash = 0;
    public string questDesc;

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
    public Text expandedQuestDesc;

    public Text levelText;
    public Slider levelSlider;
    public Text xpText;
    public Text cashText;

    private bool isExpanded;
    private bool canExpand = true;

    public AudioSource questAudioSource;
    public AudioClip[] questAudioClips;

    private void Start()
    {
        if (PlayerPrefs.GetInt("requiredXP") == 0)
        {
            PlayerPrefs.SetInt("requiredXP", 5);
        }
        if (PlayerPrefs.GetInt("lvl") == 0)
        {
            PlayerPrefs.SetInt("lvl", 1);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("effectsToggle") == 0)
        {
            questAudioSource.volume = 0;
        }
        else
        {
            questAudioSource.volume = PlayerPrefs.GetInt("volume") / 100.0f;
        }

        resetQuest();

        writeQuestDetails();

        expandDropDown();

        checkNextLevel();

        writeLevelSlider();
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

    private void resetQuest()
    {
        if (currentStep == objectives.Length && objectives.Length != 0)
        {
            hasQuest = false;
            acceptedQuest = 0;
            currentStep = 0;
            currentQuest = "";
            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + questXP);
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + questCash);
            questXP = 0;
            expandedQuestList.text = "";
            questAudioSource.clip = questAudioClips[0];
            questAudioSource.Play(); 
        }
    }
    private void writeQuestDetails()
    {
        if (hasQuest && currentStep < objectives.Length)
        {
            questText.text = currentQuest + ": " + objectives[currentStep];
            questBox.SetActive(true);

            expandedQuestTitle.text = currentQuest;

            if (expandedQuestList.text == "")
            {
                for (int i = 1; i <= objectives.Length; i++)
                {
                    expandedQuestList.text += i + ". " + objectives[i - 1] + "\n";
                }
            }

            expandedQuestDesc.text = questDesc;
        }
        else
        {
            questText.text = "";
            questBox.SetActive(false);
        }
    }
    
    private void expandDropDown()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canExpand && hasQuest)
        {
            if (!isExpanded)
                StartCoroutine("expandAnim");
            else
                StartCoroutine("unexpandAnim");

        }
    }

    private void checkNextLevel()
    {
        if (PlayerPrefs.GetInt("XP") >= PlayerPrefs.GetInt("requiredXP"))
        {
            PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") - PlayerPrefs.GetInt("requiredXP"));
            PlayerPrefs.SetInt("requiredXP", PlayerPrefs.GetInt("requiredXP") + 2 * PlayerPrefs.GetInt("lvl") + 1);
            PlayerPrefs.SetInt("lvl", PlayerPrefs.GetInt("lvl") + 1);
        }
    }

    private void writeLevelSlider()
    {
        levelSlider.value = (float) PlayerPrefs.GetInt("XP") / PlayerPrefs.GetInt("requiredXP");
        levelText.text = PlayerPrefs.GetInt("lvl").ToString();
        xpText.text = PlayerPrefs.GetInt("XP") + "/" + PlayerPrefs.GetInt("requiredXP");
        cashText.text = "$" + PlayerPrefs.GetFloat("money").ToString("F2");
    }

    public void CompleteQuestSound()
    {
        questAudioSource.clip = questAudioClips[0];
        questAudioSource.Play();
    }
}
