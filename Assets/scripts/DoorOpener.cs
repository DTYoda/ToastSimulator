using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public bool isOpen = false;
    public float rotateSpeed;
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
        else if (!isOpen && transform.eulerAngles.y > 0)
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }
    }
}
