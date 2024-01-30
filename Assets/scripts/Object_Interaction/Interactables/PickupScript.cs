using System.Collections;

using System.Collections.Generic;
using UnityEngine;



public class PickupScript : Interactable

{

    [SerializeField] private LayerMask PickupMask;

    [SerializeField] private Camera PlayerCam;

    public Transform PickupTarget;

    [Space]

    [SerializeField] private float PickupRange;

    public Rigidbody CurrentObject;

    void Start()
    {
        PickupTarget = GameObject.Find("Pick-Up Point").transform;
        //PlayerCam = Camera.main;
        PlayerCam = GameObject.Find("Playercam").GetComponent<Camera>();
    }

    public override void Interact()
    {
        HandlePickUp();
    }

    public override void RoommateInteract()
    {
    }

    public override string GetDescription()
    {
        return "click to pick up";
    }

    void HandlePickUp()
    {
        if (CurrentObject)

        {

            CurrentObject.useGravity = true;

            CurrentObject = null;

            return;

        }

        Ray CameraRay = PlayerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
{

            CurrentObject = HitInfo.rigidbody;

            CurrentObject.useGravity = false;
            CurrentObject.constraints = RigidbodyConstraints.None;
        }
    }

    public void HandleSoilPickUp(Rigidbody rb)
    {
        CurrentObject = rb;

        CurrentObject.useGravity = false;
        CurrentObject.constraints = RigidbodyConstraints.None;
        CurrentObject.constraints = RigidbodyConstraints.FreezeRotation;
    }


    private void FixedUpdate()
    {
        if(CurrentObject)
        {

            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;

            float DistanceToPoint = DirectionToPoint.magnitude;



            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint; 

        }

    }

}
    