using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private bool isOpen = false;
    private float rotateSpeed = 150;

    public Camera mainCam;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOpen = !isOpen;
        }

        if (isOpen && transform.eulerAngles.y < 90)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        else if (!isOpen && !(transform.eulerAngles.y == 0))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        if (transform.eulerAngles.y > 359)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
