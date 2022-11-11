using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    RaycastHit hit;
    public Camera mainCamera;
    public float castDistance = 2;
    private void Update()
    {
        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, castDistance);
    }
}
