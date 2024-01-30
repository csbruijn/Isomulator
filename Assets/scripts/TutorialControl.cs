using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    /// to keep track of weither our game is paused
    /// public bc it needs to be accessible from other scripts 
    /// static bc we just want to be able to easily check from other scripts if our game is paused
    /// 
    private static bool TutOverlayOpen = false;

    public GameObject TutOverlay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (TutOverlayOpen)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // when we reusme the game we want to 1. remove the pause menu
    // 2. resume game time
    //3. change pause variable to false
    private void Resume()
    {
        TutOverlay.SetActive(false);
        Time.timeScale = 1f;
        TutOverlayOpen = false;
    }

    // when we pasue the game we want to 1. bring up the pause menu
    // 2. pause time 
    // 3. change our games paused variable to true
    void Pause()
    {
        TutOverlay.SetActive(true);
        Time.timeScale = 0f;
        TutOverlayOpen = true;
    }
}
