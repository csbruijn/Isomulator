using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexController : MonoBehaviour
{

    /// to keep track of weither our game is paused
    /// public bc it needs to be accessible from other scripts 
    /// static bc we just want to be able to easily check from other scripts if our game is paused
    /// 
    public static bool CodexSheetOpen = false;

    public GameObject CodexUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CodexSheetOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
    // when we reusme the game we want to 1. remove the pause menu
    // 2. resume game time
    //3. change pause variable to false
    public void Close()
    {
        CodexUI.SetActive(false);
        CodexSheetOpen = false;
    }

    // when we pasue the game we want to 1. bring up the pause menu
    // 3. change our games paused variable to true
    private void Open()
    {
        CodexUI.SetActive(true);
        CodexSheetOpen = true;
    }
}

