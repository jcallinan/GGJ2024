using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool isHolding = false;
    private Transform objectToHold;
    private Vector3 objectOffset;
    private Transform holdingPoint; // The point where the object will be held

    public KeyCode pickUpKey = KeyCode.P;
    public float holdingDistance = 2f; // Adjust this distance to control how far the object floats in front of you

    void Start()
    {
        holdingPoint = new GameObject("HoldingPoint").transform; // Create an empty GameObject as the holding point
    }

    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            if (!isHolding)
            {
                TryPickUpObject();
            }
            else
            {
                ReleaseObject();
            }
        }

        if (isHolding)
        {
            MoveObject();
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("PickableObject"))
            {
                Debug.Log("Trying to pick up object");
                isHolding = true;
                objectToHold = hit.collider.transform;
                objectOffset = objectToHold.position - hit.point;

                // Set the holding point as the parent of the object
                objectToHold.parent = holdingPoint;

                // Optional: Disable the collider to prevent interference
                hit.collider.enabled = false;

                // Make the object kinematic while holding
                Rigidbody rb = objectToHold.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    void ReleaseObject()
    {
        Debug.Log("Releasing object");
        isHolding = false;

        // Optional: Enable the collider when releasing the object
        objectToHold.GetComponent<Collider>().enabled = true;

        // Clear the parent to release the object from the holding point
        objectToHold.parent = null;

        // Make the object non-kinematic to enable physics
        Rigidbody rb = objectToHold.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;

            // Apply a force to make the object fall
            rb.velocity = Camera.main.transform.forward * 5f; // Adjust the force as needed
        }

        objectToHold = null;
    }

    void MoveObject()
    {
        // Set the object's position relative to the camera with a constant offset
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdingDistance;
        objectToHold.position = Vector3.Lerp(objectToHold.position, targetPosition, Time.deltaTime * 5f);
    }
}