using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to set the player's walking speed
    public float turnSpeed = 3f; // Adjust this to set the turn speed
    public float maxLookUpAngle = 80f; // Maximum angle to look up
    public float maxLookDownAngle = 80f; // Maximum angle to look down
    public float pickupRange = 2f;

    private CharacterController characterController;
    private GameObject heldItem;
    private bool isHolding = false;

    private float currentRotationX = 0;

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

        if (Input.GetMouseButtonDown(0) && !isHolding)
        {
            TryPickUp();
        }
        else if (Input.GetMouseButtonDown(0) && isHolding)
        {
            DropItem();
        }

        if (isHolding)
        {
            MoveItem();
        }
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
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
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

    void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                heldItem = hit.collider.gameObject;
                isHolding = true;

                // Disable Rigidbody gravity while holding the item
                heldItem.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    void MoveItem()
    {
        // Get the position in the middle of the camera's view
        Vector3 middleOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, pickupRange));

        // Move the held item to the middle of the screen
        heldItem.transform.position = middleOfScreen;

        // Rotate the held item based on player's rotation (optional)
        heldItem.transform.rotation = transform.rotation;
    }

    void DropItem()
    {
        isHolding = false;

        // Enable Rigidbody gravity when dropping the item
        heldItem.GetComponent<Rigidbody>().useGravity = true;

        // Release the reference to the held item
        heldItem = null;
    }
}
