
using Unity.VisualScripting;
using UnityEngine;
 

public class SpatialUIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float spaceY;

    private Transform cam; 
    private Vector3 myLocation;
    public GameObject DialogueBox; 
    public TMPro.TextMeshProUGUI textBox;


    [SerializeField] private float dialogueTime;
    [SerializeField] private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
       
        cam = GameObject.Find("Playercam").transform;
        //CreateTextBox("ik wil leven");
    }

    private void Update()
    {
        if (DialogueBox.activeInHierarchy)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                DialogueBox.SetActive(false);
            }
            
        }
        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        myLocation.x = target.position.x;
        myLocation.z = target.position.z;
        myLocation.y = target.position.y + spaceY;
        transform.position = myLocation;

        transform.LookAt(cam);
        //transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector2.up);
    }


    public void CreateTextBox(string _myText)
    {
        if (!DialogueBox.activeInHierarchy)
        {
            textBox.text = _myText;
            DialogueBox.SetActive(true);
            timer = dialogueTime;
        }

    }
}

