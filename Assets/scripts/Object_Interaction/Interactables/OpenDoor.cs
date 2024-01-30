using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    [Header("Energy Usage")]
    private GameBehaviour GameManager;
    public float EnergyUseAmount;

    [Header("Put the code on the Door")]
    public Animator m_animator;

    [Header("Audio")]
   public AudioSource audioSource;  
   public AudioClip m_sound; 

   [Header("Start")]
   public bool isConsuming;

    //[SerializeField] private Light m_FridgeLight;


   private bool neverOpened = true; 
    
    // room for variables 

     

    void Start(){
    UpdateDoor();
    GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
    }

    void UpdateDoor(){
        //Open the door
        if(m_animator != null){
            if (isConsuming) {
                m_animator.SetTrigger("TrOpen");
            }
            else {
                m_animator.SetTrigger("TrClose");
            }
            //play Sound
             if (m_sound.name == "sound_door") 
            {
                
                if (neverOpened == true)
                {
                    
                    neverOpened = false; 
                }
                else
                {
                    audioSource.PlayOneShot(m_sound, 1);
                }
            }
            else if(m_sound.name == "sound_fridge")
            {
                //audioSource.Clip= m_sound;
                if(isConsuming)
                {
                    audioSource.Play();
                    audioSource.volume = 1f;
                    //m_FridgeLight.SetActive(isConsuming);
                }
                else 
                {
                    audioSource.volume = .1f;
                   // m_FridgeLight.SetActive(isConsuming);
                }
            }
            
        }       
    }

   


    public override void Interact() 
    {
        isConsuming = !isConsuming;
        if (GameManager != null)
        {
            if (isConsuming)
            {
                GameManager.totalEnergyUse += EnergyUseAmount;
            }
            else
            {
                GameManager.totalEnergyUse -= EnergyUseAmount;
            }
        }
        UpdateDoor();
    }

    public override void RoommateInteract()
    {
        if (!isConsuming) 
            {
            isConsuming = !isConsuming;
            if (GameManager != null)
            {
                if (isConsuming)
                {
                    GameManager.totalEnergyUse += EnergyUseAmount;
                }
                else
                {
                    GameManager.totalEnergyUse -= EnergyUseAmount;
                }
            }
            UpdateDoor();
        }
        
    }


    public override string GetDescription() {
    if (isConsuming) return "Click to  <color=red>close</color> the door.";
        return "Click to  <color=green>open</color> the door.";
    }
}


/* 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    private Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void DoorUpdate()
    {
        if(m_animator != null){
            if(Input.GetKeyDown(KeyCode.F))
            {
                m_animator.SetTrigger("TrOpen");
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                m_animator.SetTrigger("TrOpen");
            }
        }
    }
}
*/