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
    void Update()
    {

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

        
        if (!isHolding)
        {
            if (previousHit != null)
            {
                previousHit.GetComponent<Rigidbody>().useGravity = true;
                previousHit.GetComponent<Rigidbody>().freezeRotation = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isHolding)
        {
            previousHit.GetComponent<Rigidbody>().useGravity = false;
            previousHit.GetComponent<Rigidbody>().freezeRotation = true;
            previousHit.GetComponent<Rigidbody>().MovePosition(previousHit.transform.position + (((mainCamera.transform.position + (mainCamera.transform.forward * (castDistance - 1))) - previousHit.transform.position) * Time.deltaTime * itemSpeed));
            previousHit.transform.eulerAngles = new Vector3(previousHit.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, previousHit.transform.eulerAngles.z);
            
        }
    }
}
