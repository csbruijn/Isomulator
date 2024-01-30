using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FuelBehaviour : MonoBehaviour
{

    //public TMPro.TextMeshProUGUI fuelText;
    private GameBehaviour GameManager;
    //private float percentageFuel = 100f;

    public  GameObject[] fuelImages;

    private float fuelIncrement;

    [SerializeField] private CanvasGroup exclemationMark;

    [SerializeField] private float flashSpeed;

    [SerializeField] private float flashWarningLimit = 15f;

    private bool fadeIn = true;

    public void Update() 
    {
        if (GameManager.fuelAmount < flashWarningLimit)
        {
            Debug.Log("FLASSHH");
            FlashScreen(exclemationMark, flashSpeed);

        }
        else 
        {
            exclemationMark.alpha = 0f;
        }
    }

    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        
    }

    public void UpdateFuel()
    {
        fuelIncrement = (100 / fuelImages.Length);

        for (int i = 0; i < fuelImages.Length; i++)
        {
            if ( GameManager.fuelAmount > (fuelIncrement * i) /*+ fuelIncrement*/ )
            {
                if (!fuelImages[i].activeSelf)
                {
                    fuelImages[i].SetActive(true);
                    
                }
                

            }
            else
            {
                if (fuelImages[i].activeSelf)
                {
                    fuelImages[i].SetActive(false);
                }

            }
        }
        //Debug.Log("Fuel:" + GameManager.fuelAmount);    
    }


    public void FlashScreen(CanvasGroup img, float fadeSpeed)
    {
        if (img.alpha >= 1)
        {
            fadeIn = false;
        }
        if (img.alpha <= .6)
        {
            fadeIn = true;
        }



        if (!fadeIn)
        {
                
            img.alpha -= Time.deltaTime * fadeSpeed;
        }
        else
        {
            img.alpha += Time.deltaTime * fadeSpeed;
                
        }

        
            
    }


}
