using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public LayerMask mask;
    private bool isLooking;
    private bool isHolding = false;
    public Camera mainCamera;

    public float itemSpeed = 20;

    public float castDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    RaycastHit hit;
    GameObject previousHit = null;
    Vector3 cameraPosition;
    void Update()
    {

        cameraPosition = mainCamera.transform.position + (castDistance - 1) * mainCamera.transform.forward;

        if (!isHolding)
        {
            isLooking = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, castDistance, mask);
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (isLooking)
            {
                isHolding = true;
                previousHit = hit.transform.gameObject;
            }
        }
        else
        {
            isHolding = false;
        }

        

    }
    private void FixedUpdate()
    {
        if (!isHolding && previousHit != null)
        {
            previousHit.GetComponent<Rigidbody>().useGravity = true;
            previousHit.GetComponent<Rigidbody>().freezeRotation = false;
            previousHit.layer = 6;
        }
        if (isHolding)
        {
            previousHit.GetComponent<Rigidbody>().useGravity = false;
            previousHit.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            previousHit.GetComponent<Rigidbody>().freezeRotation = true;
            previousHit.layer = 8;
            previousHit.GetComponent<Rigidbody>().MovePosition(previousHit.transform.position + (cameraPosition - previousHit.transform.position) * Time.fixedDeltaTime * itemSpeed);

            previousHit.transform.eulerAngles = new Vector3(previousHit.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, previousHit.transform.eulerAngles.z);

        }
    }
}
