using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask doorMask;
    public Text interactText;

    private bool isLooking;
    private bool isHolding = false;
    public Camera mainCamera;

    public float itemSpeed = 30;

    public float castDistance;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public RaycastHit hit;
    public GameObject previousHit = null;
    Vector3 cameraLookPosition;
    void Update()
    {
        //gets where the camera is looking
        cameraLookPosition = mainCamera.transform.position + (castDistance - 1) * mainCamera.transform.forward;

        //reset holding and looking variables if the object you are holding has been deleted
        if (previousHit == null)
        {
            isHolding = false;
            isLooking = false;
        }

        //if you are NOT holding an object, set the bool isLooking to if a raycast hits an object
        if (!isHolding)
        {
            isLooking = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, castDistance, mask);
        }

        //if you click and are looking at a valid object
        if (Input.GetKey(KeyCode.Mouse0) && hit.transform != null)
        {
            //if you are looking at something and it is on the pickUp layer, hold the object
            if (isLooking && hit.transform.gameObject.layer == 6)
            {
                isHolding = true;
                previousHit = hit.transform.gameObject;
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            isHolding = false;
        }

        //If uoi are looking at a valid object
        if (hit.transform != null && isLooking && hit.transform.gameObject.layer == 6)
        {
            //Turn on the text and set its value
            interactText.gameObject.SetActive(true);
            if (!isHolding)
            {
                interactText.text = "Click to Pickup " + hit.transform.gameObject.name;
            }
        }



        //checks if you are looking at or opening a door
        RaycastHit hitDoor;
        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitDoor, castDistance, doorMask);
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, castDistance, doorMask) && hitDoor.transform.gameObject.CompareTag("door"))
        {
            DoorOpener doorOpen= hitDoor.transform.gameObject.GetComponent<DoorOpener>();
            interactText.gameObject.SetActive(true);
            if (doorOpen.isOpen)
            {
                interactText.text = "Press F to Close Door";
            }
            else
            {
                interactText.text = "Press F to open Door";
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                doorOpen.isOpen = !doorOpen.isOpen;
                interactText.gameObject.SetActive(false);
            }
        }

        //if you are not looking at a door or object, do not show interact text
        if ((!isLooking && !Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, castDistance, doorMask)) || (!Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, castDistance, doorMask) && isHolding))
        {
            interactText.gameObject.SetActive(false);
        }

        //if you are holding a valid object
        if (isHolding && previousHit != null)
        {
            if (previousHit.layer == 6)
            {
                interactText.gameObject.SetActive(false);
            }
            Rigidbody rb = previousHit.GetComponent<Rigidbody>();
            //changes values about the object to prevent bugs and assist its movement
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.freezeRotation = true;
            previousHit.layer = 8;
            //changes the location of the object to the player cursor
            rb.MovePosition(previousHit.transform.position + (cameraLookPosition - previousHit.transform.position) * Time.fixedDeltaTime * itemSpeed);

            previousHit.transform.eulerAngles = new Vector3(previousHit.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, previousHit.transform.eulerAngles.z);

        }
    }

    public LayerMask mask2;
    private void FixedUpdate()
    {
        //if you just dropped an object
        if (!isHolding && previousHit != null && previousHit.gameObject.layer == 8)
        {
            //release a new raycast to see if the dropped object can still be seen
            RaycastHit hit1;
            Physics.Raycast(mainCamera.transform.position, (previousHit.transform.position - mainCamera.transform.position).normalized, out hit1, castDistance, mask2);
            
            if (hit1.transform != null)
            {
                //if you cannot still see the object, assume it is out of bounds and reset the objects position
                if (hit1.transform.gameObject != previousHit)
                {
                    previousHit.transform.position = mainCamera.transform.position;
                }
            }
            previousHit.GetComponent<Rigidbody>().useGravity = true;
            previousHit.GetComponent<Rigidbody>().freezeRotation = false;
            previousHit.layer = 6;
        }
    }
}
