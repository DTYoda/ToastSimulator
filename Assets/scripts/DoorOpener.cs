using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public bool isOpen = false;
    private float rotateSpeed = 150;

    public bool reverse;
    public bool isLocked;

    public Camera mainCam;
    private void Update()
    {
        //if the door opens in the normal direction
        if(!reverse)
        {
            //if the door is supposed to be open, but isn't, rotate
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
            //if the door is supposed to be closed, but isn't, rotate the other way
            if (!isOpen && !(transform.localEulerAngles.y == 0))
            {
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            //if the door closed too far, reset it to the closed position
            if (transform.localEulerAngles.y > 350)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (isOpen && (transform.localEulerAngles.y > 270 || transform.localEulerAngles.y == 0))
            {
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (isOpen)
                {
                    transform.localEulerAngles = new Vector3(0, 270, 0);
                }

            }

            if (!isOpen && !(transform.localEulerAngles.y == 0))
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }

            if (transform.localEulerAngles.y > 4 && transform.localEulerAngles.y < 20)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        
            
    }
}
