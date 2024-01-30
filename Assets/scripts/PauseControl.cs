using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{

    /// to keep track of weither our game is paused
    /// public bc it needs to be accessible from other scripts 
    /// static bc we just want to be able to easily check from other scripts if our game is paused
    /// 
    public static bool GameIsPaused = true;

    public GameObject PauseScreen;

    public void ExitButton()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Pause();
    }

    /// <summary>
    /// testing to see if when the game is paused, the UI codex sheets should no longer be visible
    /// </summary>
    public static bool CodexSheetOpen;
    public GameObject CodexUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
    public void Resume()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.visible = false;
    }

    // when we pasue the game we want to 1. bring up the pause menu
    // 2. pause time 
    // 3. change our games paused variable to true
    void Pause()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
        CodexUI.SetActive(false);
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

