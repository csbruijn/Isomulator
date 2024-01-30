using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    private UILineRenderer line;
    private FuelBehaviour fuel; 

    public float totalEnergyUse;
    
    public float fuelAmount;
    

    [SerializeField]
    private float FuelDrainFactor = 50;

    [SerializeField] private GameObject winCondition;

    public string looseScreen;
    public string winScreen;

    [SerializeField] private CanvasGroup img;

    private bool winGame = false;
    private bool looseGame = false;



    private void Start() 
    {
        //fuelAmount = ScriptableObject.fuel; 
        //totalEnergyUse = ScriptableObject.recentConsumption;
        line = GameObject.Find("Line").GetComponent<UILineRenderer>();
        fuel = GameObject.Find("FuelMeter").GetComponent<FuelBehaviour>();

        // create an update that doesnt go brrrrrrrrrrrrrrrrrrr 
        InvokeRepeating ("UpdateEnergy", 1f,1f);

        //img = GameObject.Find("blackground").GetComponent<CanvasGroup>();
        img.alpha = 0f; 

    }


    void Update()
    {
        if (looseGame)
        {
            EndGame(looseScreen);
        }
        if (winGame) 
        {
            EndGame(winScreen);
        }
    }

    private void UpdateEnergy() 
    {
        // create new point with the recent energy usage
        line.AddNewData(totalEnergyUse);
        if (fuelAmount > 0f){
            fuelAmount -= totalEnergyUse/FuelDrainFactor;
        }
        else
        {
            Debug.Log("You loose!");
            looseGame = true;
         

        }
        
        if (/*totalEnergyUse <= 0f && */winCondition.activeInHierarchy) 
        {
            Debug.Log("You win!");
            winGame = true;
          
        }

        fuel.UpdateFuel();
        //ScriptableObject.fuel = fuelAmount;
        //ScriptableObject.recentConsumption = totalEnergyUse;
    }

    public void EndGame(string _nextRoom)
    {

        if (img.alpha < 1)
        {
            img.alpha += Time.deltaTime * 0.4f;

        }
        else
        {
            SceneManager.LoadScene(_nextRoom);
        }
    }

}
