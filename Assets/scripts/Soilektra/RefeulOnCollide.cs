using UnityEngine;



public class RefeulOnCollide : MonoBehaviour
{
    [SerializeField]
    private float refuelAmount = 50; 
    private GameBehaviour GameManager;   // Declare GameManager variable

    private void Start()


    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();  // Initialize GameManager
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.collider.CompareTag("Tulip"))
        {
            Destroy(collision.gameObject);
            if (GameManager.fuelAmount < 100 - refuelAmount)
            {
                GameManager.fuelAmount += refuelAmount ;   // Update fuelAmount variable
            }
            else
            {
                GameManager.fuelAmount = 100;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soilektra"))
        {
            Destroy(other.gameObject);
            GameManager.fuelAmount = 100;   // Update fuelAmount variable
        }
    }
}