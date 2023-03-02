using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public Text tutorialText;
    public GameObject tutorialBox;
    private string[] questSteps = { "Find Bread", "Pick Up Bread", "Toast Bread", "Give Bread to Grandma", "Touch Grass"};
    private GameObject player;

    public void startTutorial()
    {
        player = GameObject.Find("Player");
        if (PlayerPrefs.GetInt("completeTutorial") == 0)
        {
            player.GetComponent<QuestManager>().hasQuest = true;
            player.GetComponent<QuestManager>().currentStep = 0;
            player.GetComponent<QuestManager>().objectives = questSteps;
            player.GetComponent<QuestManager>().currentQuest = "Tutorial";
            player.GetComponent<QuestManager>().acceptedQuest = 0;
            player.GetComponent<QuestManager>().questXP = 5;
            StartCoroutine("tutorial");
        }
    }

    IEnumerator tutorial()
    {
        tutorialText.gameObject.SetActive(true);
        tutorialBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        tutorialText.text = "Welcome to Toastville! A small suburban town in America.";
        yield return new WaitForSecondsRealtime(3);
        tutorialText.text = "Toastville is obseced with bread, and more importantly, what you can make with it (toast!)";
        yield return new WaitForSecondsRealtime(4);
        tutorialText.text = "Here, people live breathe and eat toast, but there's a problem...";
        yield return new WaitForSecondsRealtime(3);
        tutorialText.text = "They're running out of bread!!";
        yield return new WaitForSecondsRealtime(2);
        tutorialText.text = "So, it's your mission to... Scavange for the rest of it!";
        yield return new WaitForSecondsRealtime(3);
        tutorialText.text = "That's right, instead of trying to solve the problem, you'll just contribute to it.";
        yield return new WaitForSecondsRealtime(3);
        tutorialText.text = "But hey, survival of the fittest.";
        yield return new WaitForSecondsRealtime(2);
        tutorialText.text = "First, let's go to the kitchen to see if there's anymore bread left in the house. use WSAD to move and F to open doors.";
        Time.timeScale = 1;
        yield return new WaitUntil(questStep1);
        tutorialText.text = "Great! By the way you can also hold SHIFT to sprint. There's still some bread left! Try picking it up by clicking the mouse.";
        yield return new WaitUntil(questStep2);
        tutorialText.text = "Nice! Now bring it over to the toaster and press F to toast it.";
        yield return new WaitUntil(questStep3);
        tutorialText.text = "Good going, lastly, we need to bring the finished toast to your grandmother, she's really suffering out here with so little bread.";
        yield return new WaitUntil(questStep4);
        tutorialText.text = "Amazing work. Sadly, it won't be that simple next time. Try going outside and touching grass";
        yield return new WaitUntil(questStep5);
        PlayerPrefs.SetInt("completeTutorial", 1);
        tutorialText.text = "Nice! You just completed your first task and got some XP! You'll need XP to use certain objects out here, such as cars.";
        yield return new WaitForSeconds(3);
        tutorialText.text = "Adventure around the town and talk to some of the locals, they may have some work you can do to get XP.";
        yield return new WaitForSeconds(4);
        tutorialText.text = "Did you notice that blue thing above your grandma's head? Yeah, that means she has a task for you!";
        yield return new WaitForSeconds(4);
        tutorialText.text = "Any NPC with a blue task marker above their head has a task for you, for example, the neighbor in that purple house, they have a task for you!";
        yield return new WaitForSeconds(5);
        tutorialText.text = "Keep in mind, you can always complete tasks for your neighbors, but remember to check in with your grandma every once and a while,";
        yield return new WaitForSeconds(4);
        tutorialText.text = "You'll never know when she'll want more toast! That stuff is pretty hard to get now adays so its important you help her.";
        yield return new WaitForSeconds(4);
        tutorialText.text = "The tasks from your grandma are the main ones to worry about, they give the most XP and help advance you in your task to get as much bread as possible!";
        yield return new WaitForSeconds(4);
        tutorialText.text = "The tasks from your grandma are the main ones to worry about, they give the most XP and help advance you in your task to get as much bread as possible!";
        yield return new WaitForSeconds(4);
        tutorialText.gameObject.SetActive(false);
        tutorialBox.SetActive(false);
    }

    private bool questStep1()
    {
        if(player.GetComponent<QuestManager>().currentStep == 1)
        {
            return true;
        }
        return false;
    }
    private bool questStep2()
    {
        if(player.GetComponent<ItemPickup>().previousHit != null)
        {
            if (player.GetComponent<ItemPickup>().previousHit.name == "bread")
            {
                player.GetComponent<QuestManager>().currentStep += 1;
            }
        }
        if (player.GetComponent<QuestManager>().currentStep == 2)
        {
            return true;
        }
        return false;
    }
    private bool questStep3()
    {
        if (player.GetComponent<QuestManager>().currentStep == 3)
        {
            return true;
        }
        return false;
    }
    private bool questStep4()
    {
        if (player.GetComponent<QuestManager>().currentStep == 4)
        {
            return true;
        }
        return false;
    }
    private bool questStep5()
    {
        if (player.GetComponent<QuestManager>().currentStep == 0)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt("completeTutorial", 0);
            PlayerPrefs.SetInt("XP", 0);
            PlayerPrefs.SetInt("lvl", 0);
            PlayerPrefs.SetInt("requiredXP", 0);
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetString("quests", "");
            PlayerPrefs.SetString("questItems", "");
        }
            
    }
}
