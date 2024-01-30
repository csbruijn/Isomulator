using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class RoommateBehaviour : MonoBehaviour
{
    // set locations 
    //private GameObject _switch;

    
    [SerializeField] private List<Transform> rooms;
    [SerializeField] private List<string> roomText;


    [SerializeField] private List<Transform> Locations; //Every location in chosen room 

    [SerializeField] private Animator roommateAnimator;

    // Figure out activity intervalls 
    private float breakTime;
    [SerializeField] private float minTime = 1; 
    [SerializeField] private float maxTime = 5;
    private float time; 

    private int _locationIndex = 0; //index current location destination 
    private bool doRound = true; //bool to check whether NPC should do a round in the room 

    private NavMeshAgent _agent; //NPC agent
    private Transform lsRecent; // where should the NPc check if the light is on
    private LightSwitch nearbyLightState;   // Container for checking the script


    private GameObject idleLocation;    // Where is the NPC id;le
    private bool hasDestination = false;   // Bool to not run setting destination every time 

    private SpatialUIBehaviour speechControl;

    [SerializeField] private List<string> TextToPlayer; 
     
    
   
   
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        speechControl = GameObject.Find("RM dialogue Canvas").GetComponent<SpatialUIBehaviour>();
        InitializeNewRoom(Random.Range(0, rooms.Count));

        //InitializeNewRoom(rooms[Random.Range(0, rooms.Count)]); //initialize new room 


    }

    void Update()
    {


        NPCAnimation();

        HandleConsumingRound();
        //Debug.Log(_agent.destination);

        if(!doRound)
        {
            
            
            time += Time.deltaTime;
            if (time >= breakTime)
            {

                int newRoomIndex = Random.Range(0, rooms.Count);
                InitializeNewRoom(newRoomIndex);
                SetRandomTime();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speechControl.CreateTextBox(TextToPlayer[Random.Range(0, TextToPlayer.Count)]);
        }
    }


    void InitializeNewRoom(int _newRoom)
    {
        Locations.Clear();
        _locationIndex = 0; 
        time = 0;
        
        foreach (Transform child in rooms[_newRoom])
        {
            Locations.Add(child);
        }
        speechControl.CreateTextBox(roomText[_newRoom]);

        Transform lsRecent = Locations[0];
        nearbyLightState = lsRecent.GetComponent<LightSwitch>(); 
        Transform idleLocation = Locations[1];
        doRound = true;
    }




    void NPCAnimation()
    {
        if (doRound || hasDestination)
        {
            roommateAnimator.SetBool("IsWalking", true);
        }
        else if (!hasDestination && (_agent.remainingDistance > 0.1f))
        {
            roommateAnimator.SetBool("IsWalking", true);
        }
        else
        {
            roommateAnimator.SetBool("IsWalking", false);
        }
    }


    void HandleConsumingRound()
    {
        if (doRound) 
        { 
            if (_agent.remainingDistance < 0.4f && !_agent.pathPending)
            {
                if (Locations.Count == 0) { return; }

                if (_locationIndex != Locations.Count)
                {
                    _agent.destination = Locations[_locationIndex].position;
                    _locationIndex = (_locationIndex + 1);
                }
                else 
                {
                    //Debug.Log("Do idle once");
                    doRound = false;
                    hasDestination = false;
                    _agent.destination = Locations[1].position;
                }
            } 
        }
        else 
        {
            KeepOnLight();
        }
    }

    
    void KeepOnLight()
    {
        //Debug.Log("Focus on Light");
        if (_agent.remainingDistance < 0.4f && !_agent.pathPending)
        {
            //check if switch needs switching  and if already switching 
            if (!nearbyLightState.isConsuming)
            {
                speechControl.CreateTextBox("I need that.");
                _agent.destination = Locations[0].position;
                hasDestination = true;
            }
            else if (nearbyLightState.isConsuming && hasDestination)
            {
                _agent.destination = Locations[1].position;
                hasDestination = false;
                
            }
        }
        
    }
    
    void SetRandomTime()
        {
            breakTime = Random.Range(minTime, maxTime);
        }
     
}
