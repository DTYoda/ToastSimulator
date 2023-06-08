using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleCursorScript : MonoBehaviour
{

    public Animator anim;
    public AnimationClip clip;
    bool hasClicked;
    
    void Start()
    {
        Cursor.visible = false;
        anim.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !hasClicked)
        {
            hasClicked = true;
            StartCoroutine("Click");
        }
    }
    IEnumerator Click()
    { 
        anim.enabled = true;
        anim.SetTrigger("Smash");
        yield return new WaitForSeconds(clip.length);
        anim.enabled = false;
        hasClicked = false;
    }
}
