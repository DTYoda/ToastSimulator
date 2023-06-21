using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastBaseKickOut : MonoBehaviour
{
    public Text alertText;
    GameObject player;

    public AnimationClip fadeIn;
    public AnimationClip fadeOut;
    public Animator anim;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !PlayerPrefs.GetString("boughtItems").Contains("suit"))
        {
            StartCoroutine("Caught");
        }
    }

    IEnumerator Caught()
    {
        anim.SetTrigger("out");
        yield return new WaitForSeconds(fadeOut.length);
        player.transform.position = new Vector3(1.1f, 0.8f, 9f);
        anim.SetTrigger("in");
        yield return new WaitForSeconds(fadeIn.length);
        alertText.text = "You got caught by the gaurds! Try getting a disguise to prevent getting caught next time...";
        yield return new WaitForSeconds(3);
        alertText.text = "";
    }
}
