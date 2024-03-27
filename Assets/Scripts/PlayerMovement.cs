using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;
    public Camera cam2;
    public float camLerpSpeed;

    [Header("Movement")]
    float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Jumping")]
    public bool canJump = true;
    public float jumpCooldown;
    public float jumpHeight;
    public KeyCode jumpKey = KeyCode.Space;
    public float airSpeed;

    [Header("Sprinting")]
    public bool isSprinting = false;

    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    Vector3 feetPos;
    public float gravity;

    [Header("VFX")]
    public VisualEffect phase;









    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


    }

    private void Update()
    {
        HandleFeet();

        MyInput();

        CapSpeed();


    }

    private void FixedUpdate()
    {
        MovePlayer();

        rb.AddForce(Vector3.down * gravity * rb.mass);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(sprintKey) && verticalInput > 0f)
        {
            moveSpeed = sprintSpeed;
            LerpCam(95f);

        }
        else
        {
            moveSpeed = walkSpeed;
            LerpCam(80f);
        }

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }



    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airSpeed, ForceMode.Force);
        }




    }

    private void HandleFeet()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.3f, whatIsGround))
        {
            grounded = true;
            feetPos = hit.point;
        }
        else grounded = false;




        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);

    }

    void ResetJump()
    {
        canJump = true;
    }

    void LerpCam(float lerpTo)
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, lerpTo, Time.deltaTime * camLerpSpeed);
        cam2.fieldOfView = Mathf.Lerp(cam.fieldOfView, lerpTo, Time.deltaTime * camLerpSpeed);
    }

    void CapSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

}
