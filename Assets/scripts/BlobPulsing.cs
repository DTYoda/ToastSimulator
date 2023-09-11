using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPulsing : MonoBehaviour
{

    Vector3 originalScale;

    bool canStart = true;

    private void Start()
    {
        originalScale = this.transform.localScale;
    }

    private void Update()
    {
        if(canStart)
        {
            StartCoroutine("pulseAnim");
        }
    }

    IEnumerator pulseAnim()
    {
        canStart = false;
        for(int i = 0; i < 50; i++)
        {
            this.transform.localScale += (originalScale * .2f) / 50;
            yield return new WaitForSecondsRealtime(.001f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < 50; i++)
        {
            this.transform.localScale -= (originalScale * .2f) / 50;
            yield return new WaitForSecondsRealtime(.001f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
        canStart = true;
    }
}
