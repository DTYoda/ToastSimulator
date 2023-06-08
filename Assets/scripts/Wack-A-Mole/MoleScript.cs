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
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoleClickAnim()
    {
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(hitClip.length);
        manager.score++;
        Destroy(this.gameObject);
    }
}
