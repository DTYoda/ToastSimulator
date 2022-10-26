using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public string[] dialog;
    public GameObject player;
    public Rigidbody body;
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 7 && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(dialog[Random.Range(0, dialog.Length)]);
        }
    }
}
