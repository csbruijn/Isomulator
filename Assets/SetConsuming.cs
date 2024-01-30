using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetConsuming : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        // get/check the interactale component
        Interactable interactable = other.GetComponent<Interactable>();


        Debug.Log(" detected");
        //Check is it is not empy 
        if (interactable != null)
        {
            // make it consume
           // if (!interactable.isConsuming)
            {
                interactable.RoommateInteract();
                Debug.Log("Consume!");

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // check wheater it is an interactable
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            // reset your you turn back to your origin

        }
    }
}
