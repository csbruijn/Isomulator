using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=858X6_WHfuw
// https://www.youtube.com/watch?v=lfTyk7Xms9k&t=305s
// https://www.youtube.com/watch?v=_yf5vzZ2sYE

public class FirstPersonPlayerInteraction : MonoBehaviour
{


    public float interactionDistance; // how far away van the player interact 

    public TMPro.TextMeshProUGUI interactionText; // what text should be overwritten for the UI
    public UnityEngine.UI.Image interactionHoldProgress; // what image should be interactable
    public GameObject interactionHoldGo;


    public Transform cam;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selactableTag = "SelectableTag";

    [SerializeField] private Material defaultMaterial;
    private Transform _selection;

    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null; 
        }
        RaycastHit hit;
        bool successfulHit = false;

        // Check if there is an interactable in front of the player
        if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            var selection = hit.transform; 
            if (selection.CompareTag(selactableTag) ) { 
                var selectionRenderer = selection.GetComponent<Renderer>();

                if(selectionRenderer != null)
                {
                    defaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }

            if (interactable != null)
            {
                // get Interactable Interaction and Description
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;
                interactionHoldGo.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            }

        }

        // if we miss, hide the UI
        if (!successfulHit)
        {
            interactionText.text = "";
            interactionHoldGo.SetActive(false);
        }
    }

    //Code for interaction Key
    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = KeyCode.Mouse0; //This is the keybinding for interacting 
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                // Interaction type is Holding the button, we hold the button -> interact
                if (Input.GetKey(key))
                {
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f)
                    {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                }
                else
                {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();


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


