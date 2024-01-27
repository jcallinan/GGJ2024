using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f; // Adjust the sensitivity as needed
    public float verticalLookLimit = 90.0f; // Adjust the limit as needed

    private float rotationX = 0;

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse movement
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Calculate vertical rotation
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Cast a ray from the camera to the mouse cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the direction to look at the hit point
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0; // Ensure the camera stays level

            // Rotate the camera to look at the hit point
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
