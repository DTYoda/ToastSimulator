using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public LayerMask mask;
    private bool isLooking;
    private bool isHolding = false;
    public Camera mainCamera;

    public float itemSpeed;

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

        Debug.Log(hit.transform.position);

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

        
        if (!isHolding && previousHit != null)
        {
            previousHit.GetComponent<Rigidbody>().useGravity = true;
            previousHit.GetComponent<Rigidbody>().freezeRotation = false;
        }

        Debug.Log(cameraPosition);
        Debug.Log(previousHit.transform.position);
        if (previousHit.transform.position != cameraPosition)
        {
            Debug.Log("Not equal");
        }
        else
        {
            Debug.Log("They are equal");
        }
        if (isHolding)
        {
            previousHit.GetComponent<Rigidbody>().useGravity = false;
            previousHit.GetComponent<Rigidbody>().freezeRotation = true;
            previousHit.transform.position = cameraPosition;

            previousHit.transform.eulerAngles = new Vector3(previousHit.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, previousHit.transform.eulerAngles.z);

        }
    }
    private void FixedUpdate()
    {

    }
}
