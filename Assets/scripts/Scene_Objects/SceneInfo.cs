using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{
    public float fuel;
    public float recentConsumption;
    public float baseConsumption = 6; 
    public float startFuel = 55; 

    public bool fridge1 = true;

    void Start()
    {
        fuel = startFuel;
        recentConsumption = baseConsumption; 
        
    }


    // MAKE A LIST OF ALL CONSUMING OBJECTS 
    // HAVE EACH OBJECT BE LINKED TO A BOOL CHECKING THEIR STATE (CONSUMING OR NO)
    // 

}
