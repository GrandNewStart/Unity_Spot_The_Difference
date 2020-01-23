using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Properties")]
    public float walkSpeed;
    public float runSpeed;
    private int walkStep = 0;
    private CharacterController characterController;
    private AudioSource walk;
    private bool moving = false;

    [Header("Jump & Fall Properties")]
    public float jumpHeight = 10.0f;
    public float verticalVelocity = -2f;
    public LayerMask groundMask;
    public GameObject groundCheck = null;
    private float groundDistance = 0.4f;
    private float gravity = 9.8f;
    private bool isGrounded = true;    

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
        walk = GetComponent<AudioSource>();
    }

    public void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        float moveH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * moveH + transform.forward * moveV;
        characterController.Move(move);

        if (isGrounded && characterController.velocity.magnitude >2f && !walk.isPlaying)
        {
            walk.volume = Random.Range(0.5f, 1f);
            walk.pitch = Random.Range(0.5f, 1.1f);
            walk.Play();
            if (speed == runSpeed)
            {
                walk.Play();
            }
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(jumpVector * Time.deltaTime);
    }
 }
