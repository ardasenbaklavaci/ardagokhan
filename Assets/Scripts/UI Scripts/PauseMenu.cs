using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public static bool isGamePaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                
                
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;

    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
       Cursor.lockState = CursorLockMode.None;//needed to use the curser in the pause menu
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        InputSystem.DisableDevice(Keyboard.current);
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();//will work at built
    }
}

