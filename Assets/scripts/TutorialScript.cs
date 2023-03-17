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
            player.GetComponent<QuestManager>().questDesc = "Look here to see more information about tasks you do!";
            player.GetComponent<QuestManager>().questCash = 5;
            StartCoroutine("tutorial");
        }
    }

    IEnumerator tutorial()
    {
        tutorialText.gameObject.SetActive(true);
        tutorialBox.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
        tutorialText.text = "Welcome to Toast Town, USA! A small suburban town in America. (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Toast Town is obsessed with bread, and more importantly, what you can make with it (toast!)  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Here, people live breathe and eat toast, but there's a problem...  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "They're running out of bread!! There's some sort of shortage going around.  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "So, it's your mission to get to the bottom of it!  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Nobody quite knows what this shortage is caused from, but with enough looking, I'm sure you can find it, and fix it.  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Why should you care, you ask? Well, because your poor grandmother is in desperate need of some toast!  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "First, let's go to the kitchen to see if there's anymore bread left in the house. use WASD to move, F to open doors, and SHIFT to sprint.";
        Time.timeScale = 1;
        yield return new WaitUntil(questStep1);
        tutorialText.text = "Great! By the way you can press Q to see more details about your current quest. The menu will show you all of the quest's tasks, and the bottom of the menu will remind you of the description of the quest!  (Click to continue)";
        yield return new WaitForSecondsRealtime(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = " Look! There's still some bread left! Try picking it up by clicking the mouse.";
        yield return new WaitUntil(questStep2);
        tutorialText.text = "Nice! Now bring it over to the toaster and press F while holding the bread and looking at the toaster to toast it.";
        yield return new WaitUntil(questStep3);
        tutorialText.text = "Good going, lastly, we need to bring the finished toast to your grandmother, she's really suffering out here with so little bread.";
        yield return new WaitUntil(questStep4);
        tutorialText.text = "Amazing work. Sadly, there isn't anymore bread, but that's where you come in. The first step to your journey is going outside and touching grass";
        yield return new WaitUntil(questStep5);
        PlayerPrefs.SetInt("completeTutorial", 1);
        tutorialText.text = "Nice! You just completed your first task and got some XP and money! You'll need XP to use certain objects, and money to buy whatever tools you need to fix the shortage.  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Adventure around the town and talk to some of the locals, they may have some work you can do, and more importantly, they may know something about the bread shortage.  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Did you notice that blue thing above your grandma's head? Yeah, that means she has a task for you! NPCs with blue markers have tasks  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Keep in mind, you can always complete tasks for your neighbors, but remember to search around the town for any clues.  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "You'll never be able to solve the toast crisis without finding clues, even if you do collect a TON of money.  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
        tutorialText.text = "Make sure to talk to NPCs, even if they don't have a task for you, they could have very important information, and you could learn a bit more about your neighbors.  (Click to continue)";
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(Clicked);
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

    private bool Clicked()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }
}
