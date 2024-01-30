using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FanControl : Interactable
{
    private GameBehaviour GameManager; 
    [SerializeField]private bool isConsuming;
    [SerializeField] private float EnergyUseAmount;
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject windParticles;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateFan();
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
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
        UpdateFan();
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
            UpdateFan();
        }
    }

    void UpdateFan()
    {
        m_animator.SetBool("isConsuming", isConsuming);
        windParticles.SetActive(isConsuming);
    }


    // Update is called once per frame
    public override string GetDescription()
    {
        if (isConsuming) return "Click to turn <color=red>off</color> the fan.";
        return "Click to turn <color=green>on</color> the fan.";
    }
}
