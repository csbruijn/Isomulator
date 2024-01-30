using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using static UnityEngine.GraphicsBuffer;

public class TopDownCamera : MonoBehaviour
{
    [Header("Cam target")]
    [SerializeField] private Transform m_Target;
    [SerializeField] private Transform m_Player;
    [SerializeField] private Transform m_Focus;
    [SerializeField] private bool focusOn;

    [Header("Cam numbers")]
    [SerializeField] private float m_Height = 90f; 
    [SerializeField] private float m_Distance = 10f; 
    [SerializeField] private float m_Angle = 0f; 
    [SerializeField] private float m_smoothSpeed = .1f; 
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float minZoom = 91f;
    [SerializeField] private float maxZoom = 100f;

    [SerializeField] private float xBound = 5f;
    [SerializeField] private float zBound = 5f;
    //[SerializeField] private float CamMoveSpeed = 10;

    private float camXPos;
    private float camYPos;
    private float camZPos;

    private Vector3 targetPosition; 
    //private bool IsInView = true;


    // smoothing and scrolling
    private Vector3 refVelocity;
    private float HeDiRatio; 
    

    // Start is called before the first frame update
    void Start()
    {
        var camXPos = transform.position.x;
        var camYPos = transform.position.y + m_Height;
        var camZPos = transform.position.z - m_Distance;
        
        HandleCamera();
        HeDiRatio = m_Height/m_Distance; 
    }

    // Update is called once per frame
    void Update()
    {

        if (focusOn) 
        {
            // focus cam on house
            CamFocus();
        }
        

        
        
        
    }



    private void LateUpdate()
    {
        HandleCameraBound();
        HandleCamera();
        // scrolling adjust cam
        HandleZoom();
            
        

    }

    protected virtual void HandleCamera()
    {
        if(!m_Target)
        {
            return; 
        }

        

        // build world position vector 
        Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height); 
        
        
        Debug.DrawLine(m_Target.position , worldPosition, Color.red);

        //build rotated vector 
        Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition; 
        Debug.DrawLine(m_Target.position , rotatedVector, Color.green);

        //move our position 
        Vector3 flatTargetPosition = m_Target.position;
        flatTargetPosition.y = 0f; 
        Vector3 finalPosition = flatTargetPosition + rotatedVector; 
        Debug.DrawLine(m_Target.position, finalPosition, Color.blue);

        // cam smoothing
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_smoothSpeed * Time.deltaTime);

        // rotate cam to center player
        transform.LookAt(m_Target.position);

        

    }

    private void CamFocus()
    {

        // rotate Target to face Focus without tilting
        Vector3 targetPosition = new Vector3
            (
            m_Focus.position.x,
            m_Target.position.y,
            m_Focus.position.z
            );
        m_Target.LookAt(targetPosition);

        m_Player.LookAt(targetPosition);

        //m_Target.LookAt(m_Focus);
        m_Angle = m_Target.eulerAngles.y;

    
    }

    private void HandleZoom()
    {
        m_Height -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        m_Height = Mathf.Clamp(m_Height, minZoom, maxZoom);
        m_Distance -= (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed)/HeDiRatio;
        m_Distance = Mathf.Clamp(m_Distance, minZoom/HeDiRatio, maxZoom/HeDiRatio);
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        if(m_Target)
        {
            Gizmos.DrawLine(transform.position, m_Target.position);
            Gizmos.DrawSphere(m_Target.position, 1.5f);
        }
        Gizmos.DrawSphere(transform.position, 1.5f);

    }
       
    private void HandleCameraBound()
    {
        float xSeperation = m_Player.position.x - m_Target.position.x;
        float zSeperation = m_Player.position.z - m_Target.position.z;

        if (xSeperation > xBound || xSeperation < -xBound)
        {
            // player is outside x bound
            if (xSeperation > 0)
            {
                //Debug.Log("player is on the right");
                xSeperation -= xBound;
            }
            else
            {
                xSeperation += xBound;
                //Debug.Log("player is on the left");
            }
        }
        else
        {
            xSeperation = 0;
        }
        
        if (zSeperation > zBound || zSeperation < -zBound) 
        {
            // player is outside z bound 
            if (zSeperation > 0)
            {
                zSeperation -= zBound;
                //Debug.Log("player is on the up");
            }
            else
            {
                zSeperation += zBound;
                //Debug.Log("player is on the down");
            }
        }
        else
        {
            zSeperation = 0;
        }

        Vector3 newCamPos = new Vector3(m_Target.position.x + xSeperation, m_Target.position.y, m_Target.position.z + zSeperation);
        m_Target.position = Vector3.SmoothDamp(m_Target.position, newCamPos, ref refVelocity, m_smoothSpeed);

    }
}






