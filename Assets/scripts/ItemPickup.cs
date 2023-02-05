using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public LayerMask mask;
    public Text interactText;

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
    public GameObject previousHit = null;
    Vector3 cameraPosition;
    void Update()
    {
        if (previousHit == null)
        {
            isHolding = false;
        }

        cameraPosition = mainCamera.transform.position + (castDistance - 1) * mainCamera.transform.forward;

        if (!isHolding)
        {
            isLooking = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, castDistance, mask);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
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

        if(isLooking && hit.transform.gameObject.CompareTag("door"))
        {
            DoorOpener doorOpen= hit.transform.gameObject.GetComponent<DoorOpener>();
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
            previousHit = hit.transform.gameObject;
        }

        if (isLooking && hit.transform.gameObject.layer == 6)
        {
            interactText.gameObject.SetActive(true);
            if (!isHolding)
            {
                interactText.text = "Click to Pickup " + hit.transform.gameObject.name;
            }
        }

        if ((previousHit != null && !isLooking))
        {
            interactText.gameObject.SetActive(false);
        }

        if (!isHolding && previousHit != null)
        {
            if (previousHit.gameObject.layer == 8)
            {
                previousHit.GetComponent<Rigidbody>().useGravity = true;
                previousHit.GetComponent<Rigidbody>().freezeRotation = false;
                previousHit.layer = 6;
            }
        }

    }
    private void FixedUpdate()
    {
        if (isHolding)
        {
            if(previousHit.layer == 6)
            {
                interactText.gameObject.SetActive(false);
            }
            previousHit.GetComponent<Rigidbody>().useGravity = false;
            previousHit.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            previousHit.GetComponent<Rigidbody>().freezeRotation = true;
            previousHit.layer = 8;
            previousHit.GetComponent<Rigidbody>().MovePosition(previousHit.transform.position + (cameraPosition - previousHit.transform.position) * Time.fixedDeltaTime * itemSpeed);

            previousHit.transform.eulerAngles = new Vector3(previousHit.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, previousHit.transform.eulerAngles.z);

        }
    }
}
