using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
{
    EventManagerMole manager;
    public Animator anim;
    public AnimationClip dieClip;
    public AnimationClip hitClip;

    public bool clicked = false;
    void Start()
    {
        manager = GameObject.Find("EventSystem").GetComponent<EventManagerMole>();
    }

    private void Awake()
    {
        StartCoroutine("SelfDustruct");
    }
    public void MoleClick()
    {
        if(!clicked)
        {
            anim.SetTrigger("Hit");
            StartCoroutine("MoleClickAnim");
        }
        clicked = true;
    }

    IEnumerator SelfDustruct()
    {
        yield return new WaitForSeconds(Random.Range(300, 2000) / 1000.0f);
        if(!clicked)
        {
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(dieClip.length);
            if(!clicked)
                Destroy(this.gameObject);
        }
    }

    IEnumerator MoleClickAnim()
    {
        manager.score++;
        yield return new WaitForSeconds(hitClip.length);
        Destroy(this.gameObject);
    }
}
