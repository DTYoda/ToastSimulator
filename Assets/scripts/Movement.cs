using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float originalSpeed = 4;
    private float sprintSpeed = 7;
    private bool isSprinting = false;

    public float jumpHeight;

    public Rigidbody playerBody;
    public GameObject player;
    public Camera mainCamera;

    public Vector3 direction;

    public GameObject groundCheck;
    private float checkRadius = 0.01f;
    public bool isGrounded;

    public LayerMask mask;

    public Animator charAnim;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerBody.MovePosition(playerBody.position + direction * Time.deltaTime);

    }
    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (direction.magnitude > 0)
        {
            charAnim.SetBool("isWalking", true);
        }
        else
        {
            charAnim.SetBool("isWalking", false);
        }

        if (direction.magnitude > 1)
        {
            direction /= direction.magnitude;
        }
        direction *= speed;
        direction = playerBody.rotation * direction;

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, mask);

        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = originalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerBody.AddForce(new Vector3(0, jumpHeight, 0));
            charAnim.SetTrigger("Jump");
        }
    }
}
