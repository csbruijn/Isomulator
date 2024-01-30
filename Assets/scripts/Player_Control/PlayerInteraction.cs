using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// https://www.youtube.com/watch?v=858X6_WHfuw
// https://www.youtube.com/watch?v=lfTyk7Xms9k&t=305s

public class PlayerInteraction : MonoBehaviour {


    public float interactionDistance; // how far away van the player interact 

    public TMPro.TextMeshProUGUI interactionText; // what text should be overwritten for the UI

    public Camera cam;

    private GameObject targetDest;
    private NavMeshAgent player;
    private Animator playerAnimator;
    //private Material originalMaterial;
    //public Material highlighMaterial;
    private float distance;
    //private Interactable highlightable;


    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        targetDest = GameObject.Find("Target Destination");
        //originalMaterial = GetComponent<Renderer>().material;

    }


    void Update() {

        //PlayerAnimation();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                distance = Vector3.Distance(hit.transform.position, transform.position);
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null && distance <= interactionDistance)
                {
                    Debug.Log("Do the interaction");
                    interactable.Interact();
                }
                else
                {
                    // Move to the point
                    targetDest.transform.position = hit.point;
                    player.SetDestination(hit.point);
                }
            }
        }

        //HandleHighlight();

        
    }




    void PlayerAnimation()
    {
        
        if (player.velocity != Vector3.zero && playerAnimator != null)
        {
           
            playerAnimator.SetBool("IsWalking", true);
        }
        else if (player.velocity == Vector3.zero && playerAnimator != null)
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }


        
    //void HandleHighlight()
    //{
    //    if (distance <= interactionDistance)
    //    {
    //        // Apply the highlight material
    //    }
    //    else
    //    {
    //        // Restore the original material
    //        GetComponent<Renderer>().material = originalMaterial;
    //    }
    //}

    //Code for interaction Key
    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = KeyCode.F; //This is the keybinding for interacting 
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact

                interactable.Interact();

                break;
            case Interactable.InteractionType.Hold:
                // Interaction type is Holding the button, we hold the button -> interact
                if (Input.GetKey(key))
                {

                }

                break;
            // here is started code for our  custom interaction :)
            case Interactable.InteractionType.Minigame:
                // here you make ur minigame appear
                break;
            // If it is not working properly this is a helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

}


