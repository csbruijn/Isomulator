using UnityEngine;

public class Range : MonoBehaviour
{
    public float pickupRange = 3f;
    public KeyCode pickupButton = KeyCode.Space;
    public LayerMask pickupLayer;

    private Rigidbody pickedUpRigidbody;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform;
    }

    private void Update()
    {
        // Check for player input
        if (Input.GetKeyDown(pickupButton))
        {
            // Check if currently holding a Rigidbody
            if (pickedUpRigidbody != null)
            {
                // Drop the Rigidbody
                Drop();
            }
            else
            {
                // Try to pick up a Rigidbody
                TryPickup();
            }
        }
    }

    private void TryPickup()
    {
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, pickupRange, pickupLayer); // Use Physics.OverlapSphere with layer mask

        // Iterate through all colliders and check if they have a Rigidbody component
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Pick up the Rigidbody
                PickUp(rb);
                Debug.Log("Picked up Rigidbody: " + rb.name);
                break; // Stop searching for Rigidbodies after picking up one
            }
        }
    }

    private void PickUp(Rigidbody rb)
    {
        pickedUpRigidbody = rb;
        rb.isKinematic = true;
        rb.transform.SetParent(playerTransform);
    }

    private void Drop()
    {
        pickedUpRigidbody.isKinematic = false;
        pickedUpRigidbody.transform.SetParent(null);
        pickedUpRigidbody = null;
    }
}