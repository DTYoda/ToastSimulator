using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float originalSpeed;
    public float jumpSpeed;
    public float sprintSpeed;

    public float jumpHeight;

    public Rigidbody playerBody;
    public GameObject player;
    public Camera mainCamera;

    public Vector3 direction;

    public GameObject groundCheck;
    public float checkRadius;
    public bool isGrounded;

    public LayerMask mask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerBody.MovePosition(playerBody.position + direction * Time.deltaTime);

    }
    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (direction.magnitude > 1)
        {
            direction /= direction.magnitude;
        }
        direction *= speed;
        direction = playerBody.rotation * direction;

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, mask);
        if (!isGrounded)
        {
            speed = jumpSpeed;
        }
        else 
        {
            speed = originalSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
                playerBody.AddForce(new Vector3(0, jumpHeight, 0));
        }

    }
}
