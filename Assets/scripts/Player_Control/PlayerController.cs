using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody heldObject;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (heldObject == null) {
                // Try to pick up an object
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 25f)) {
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    if (rb != null) {
                        heldObject = rb;
                        heldObject.isKinematic = true;
                        heldObject.transform.SetParent(transform);
                        heldObject.transform.localPosition = Vector3.forward;
                    }
                }
            } else {
                // Drop the held object
                heldObject.isKinematic = false;
                heldObject.transform.SetParent(null);
                heldObject = null;
            }
        }
    }
}