using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : Interactable {
   
   // public AudioClip m_sound;
    //public AudioSource audioSource;

   [SerializeField] GameObject intact;
   [SerializeField] GameObject broken;
   BoxCollider bc;


    private void Awake() 
    {   
        intact.SetActive(true);
        broken.SetActive(false);
        bc = GetComponent<BoxCollider>();  
    }

    public override string GetDescription() 
    {
        return "Press [F] to turn break the glass.";
    }

    public override void Interact() 
    {
        //audioSource.PlayOneShot(m_sound,1);
        Break();
    }

    public override void RoommateInteract()
    {
        //audioSource.PlayOneShot(m_sound,1);
        Break();
    }

    private void Break()
    {
        intact.SetActive(false);
        broken.SetActive(true);
        bc.enabled = false;
    }

}
