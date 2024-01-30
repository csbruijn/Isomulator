using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LightSwitch : Interactable {
    
    [Header("Energy Usage")]
    private GameBehaviour GameManager;
    public float EnergyUseAmount; 

    [Header("Connected Light")]
    public Light m_Light;

    [Header("Audio")]
    public AudioClip m_sound;
    private AudioSource audioSource;
    
    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Renderer objectToChange;
    
    
    //private GameObject player;
    //public int maxDistance = 5; 

 [Header("Start")]
    public bool isConsuming;

    private void Start() {
        UpdateLight();
        emissiveMaterial = objectToChange.GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
        //player = GameObject.Find("playerObj");
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
    
    }

    void UpdateLight() {
        m_Light.enabled = isConsuming;
    }

    void UpdateEmission() {
        if(isConsuming) {
            emissiveMaterial.EnableKeyword("_EMISSION");
        }
        else{
        emissiveMaterial.DisableKeyword("_EMISSION");
        }
    }

    public override string GetDescription() {
        if (isConsuming) return "Click to turn <color=red>off</color> the light.";
        return "Click to turn <color=green>on</color> the light.";
    }

    public override void Interact() {
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

        audioSource.PlayOneShot(m_sound,1);
        UpdateLight();
        UpdateEmission();
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

            audioSource.PlayOneShot(m_sound, 1);
            UpdateLight();
            UpdateEmission();
        }
        
    }


}


