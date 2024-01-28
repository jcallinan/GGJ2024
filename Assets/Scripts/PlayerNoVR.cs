using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to set the player's walking speed
    public float turnSpeed = 3f; // Adjust this to set the turn speed
    public float maxLookUpAngle = 80f; // Maximum angle to look up
    public float maxLookDownAngle = 80f; // Maximum angle to look down
    public float gravity = 9.8f; // Adjust this value as needed
    public float groundRaycastDistance = 0.2f; // Adjust this based on your ground height

    private CharacterController characterController;
    private GameObject heldItem;
    private bool isHolding = false;

    private float currentRotationX = 0;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        LookWithMouse();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        // Apply gravity
        if (!IsGrounded())
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            // Reset velocity when grounded to prevent accumulating gravity over time
            velocity.y = -2f;
        }

        // Move the player using the CharacterController
        characterController.Move((moveDirection * moveSpeed + velocity) * Time.deltaTime);
    }

    void LookWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        // Rotate the player based on mouse movement
        transform.Rotate(Vector3.up * mouseX * turnSpeed);

        // Calculate vertical rotation
        currentRotationX += mouseY * turnSpeed;
        currentRotationX = Mathf.Clamp(currentRotationX, -maxLookUpAngle, maxLookDownAngle);

        // Apply the rotation to the camera
        Camera.main.transform.localRotation = Quaternion.Euler(currentRotationX, 0, 0);
    }

    bool IsGrounded()
    {
        // Raycast to check if the player is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRaycastDistance))
        {
            return true;
        }

        return false;
    }
}
