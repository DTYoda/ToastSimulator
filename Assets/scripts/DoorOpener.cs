using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public bool isOpen = false;
    private float rotateSpeed = 150;

    public Camera mainCam;
    private void Update()
    {
        if (isOpen && transform.localEulerAngles.y < 90)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (isOpen)
            {
                transform.localEulerAngles = new Vector3(0, 90, 0);
            }

        }
        
        if (!isOpen && !(transform.localEulerAngles.y == 0))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
        
        if (transform.localEulerAngles.y > 350)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
            
    }
}
