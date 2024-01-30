using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=gtpXc_9MR6g 

public class SceneTrans : MonoBehaviour
{
    // TP info
    public Transform TPtarget;
    private Vector3 TP;

    [SerializeField] private bool onlyPlayer = true;


    //Player info
    [SerializeField] private GameObject player;
    private Rigidbody rb; 
    
    void Start()
    {
        //player = GameObject.Find("playerObj");
        rb = player.GetComponent<Rigidbody>(); Debug.Log("RB set"); 
        TP = TPtarget.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer) 
        {
            if (other.CompareTag("Player"))
            {
                TeleportPlayer();
            }
        }
        else
        {
            
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.transform.position = TP;
            
        }
        
    }

    void TeleportPlayer()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; 
        player.transform.position = TP;
    }

        //public string scenename;

        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        SceneManager.LoadScene(scenename);
        //    }

        //}


    }
