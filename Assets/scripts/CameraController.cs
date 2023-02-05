using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float minX = -60f;
	public float maxX = 60f;

	public float sensitivity;
	public Camera cam1;
	public Camera cam2;

	float rotY = 0f;
	float rotX = 0f;

	void Update()
    {
        sensitivity = PlayerPrefs.GetInt("sens") / 5;

        rotY += Input.GetAxis("Mouse X") * sensitivity;
		rotX += Input.GetAxis("Mouse Y") * sensitivity;

		rotX = Mathf.Clamp(rotX, minX, maxX);
	
		if (Cursor.visible && Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
    private void FixedUpdate()
    {
		if(Time.timeScale == 1)
        {
			transform.localEulerAngles = new Vector3(0, rotY, 0);
			cam1.transform.localEulerAngles = new Vector3(-rotX, 0, 0);
			cam2.transform.localEulerAngles = new Vector3(-rotX, 0, 0);
		}

	}
    public void resetXY(float x, float y)
    {
		rotY = y;
		if(x > 180)
        {
			rotX = -x + 360;
        }
		else
        {
			rotX = -x;
        }
    }

}
