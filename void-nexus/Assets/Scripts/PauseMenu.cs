using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private InputActionProperty aButton;
    [SerializeField] private InputActionProperty bButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string mainMenuSceneName = "Menu";
    [SerializeField] private predicion predl;
    [SerializeField] private predicion predr;

    private bool isPaused = false;

    private void OnEnable()
    {
        aButton.action.performed += Pause;
        bButton.action.performed += ExitToMainMenu;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        //Debug.Log(isPaused);
    }


    private void Pause(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
            isPaused = true;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
                predl.enabled = false;
                predr.enabled = false;
            }
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Unpaused");
            isPaused = false;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
                predl.enabled = true;
                predr.enabled = true;
            }
        }
    }



    public void Continue()
    {
        Time.timeScale = 1;
        Debug.Log("continue");
        isPaused = false;
        
    }
    public void ExitToMainMenu(InputAction.CallbackContext context)
    {
        if(isPaused == true && Time.timeScale == 0f)
        {
            Time.timeScale = 1;
            Debug.Log("Loading Main Menu");
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

}





    //private void Update()
    //{
    //    if (isPaused && Input.GetKeyDown(KeyCode.Escape)) // Предполагается, что вы используете клавишу Escape для выхода из паузы
    //    {
    //        Resume();
    //    }
    //}

   