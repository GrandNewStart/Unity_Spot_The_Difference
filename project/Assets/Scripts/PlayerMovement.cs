using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private CharacterController characterController;

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        Move();
    }

    void Move()
    {
        float moveH = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 move = transform.right * moveH + transform.forward * moveV;
        characterController.Move(move);
    }
}
