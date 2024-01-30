using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveWASD : MonoBehaviour
{

    private Vector2 moveInput;
    private Vector2 mouseDelta;
    private CharacterController characterController; 

    private void Awake()
    {
        // Get character controller 
        characterController = GetComponent<CharacterController>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OnMove();
        //OnLook();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        Debug.Log(moveInput);

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
