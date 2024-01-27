using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Adjust this to set the player's walking speed
    public float velocity = 0f;
    public float turnSmoothTime;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveVector * speed * Time.deltaTime);
        }
    }
}

