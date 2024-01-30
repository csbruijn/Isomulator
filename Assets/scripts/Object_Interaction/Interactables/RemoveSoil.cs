using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSoil : Interactable
{
    [SerializeField] private GameObject StaticSoil;
    [SerializeField] private GameObject LooseSoil;

    private PickupScript soilInteract;


    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    public void Remove()
    {
        
        LooseSoil.SetActive(true);
     
        soilInteract = LooseSoil.GetComponent<PickupScript>();
        soilInteract.HandleSoilPickUp(LooseSoil.GetComponent<Rigidbody>());

        StaticSoil.SetActive(false);

        //soilInteract.CurrentObject = LooseSoil.GetComponent<Rigidbody>(); 
        //LooseSoil.transform.position = soilInteract.PickupTarget.position; // move it to the pickuppoint so that the pickup scripts detects an object
        //soilInteract.Interact();


    }

    public void Initialize()
    {
        StaticSoil.SetActive(true);
        LooseSoil.SetActive(false);
    }

    public override void Interact()
    {
        Remove();
    }

    public override void RoommateInteract()
    {
        return;// null
    }

    public override string GetDescription()
    {
        return "click to remove";
    }
}


