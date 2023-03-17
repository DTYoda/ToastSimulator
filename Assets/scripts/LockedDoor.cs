using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public ItemPickup pickup;

    private void Start()
    {
        pickup = GameObject.Find("Player").GetComponent<ItemPickup>();
    }
    private void Update()
    {
        if (pickup.hitDoor.transform != null && pickup.hitDoor.transform.gameObject == this && !PlayerPrefs.GetString("boughtItems").Contains("Lock Pick"))
        {
            pickup.interactText.text = "Lockpick required!";
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
