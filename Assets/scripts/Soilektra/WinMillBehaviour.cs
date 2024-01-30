using System.Collections.Generic;
using UnityEngine;

public class WinMillBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Repaired;
    [SerializeField] private GameObject Broken;

    private List<Transform> parts = new List<Transform>();
    private int repairIndex = 0;
    private int repairRequired;



    // Start is called before the first frame update
    void Awake()
    {
        Break();
    }

    private void Start()
    {
        foreach (Transform child in Broken.transform)
        {
            parts.Add(child);
        }

        repairRequired = parts.Count - 2;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (parts.Contains(collision.transform)) 
        {
            if(repairIndex < repairRequired) 
            {
                Debug.Log("Part added");
                Destroy(collision.gameObject);
                repairIndex += 1;
            }
            else // if(repairIndex == repairRequired) 
            {
                Destroy(collision.gameObject);
                
                Debug.Log("Last Part added");
                Repair();

            }



        }
    }



    public void Repair()
    {
        Repaired.SetActive(true);
        Broken.SetActive(false);
    }

    public void Break()
    {
        Repaired.SetActive(false);
        Broken.SetActive(true);
    }
}
