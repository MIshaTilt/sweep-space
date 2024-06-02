using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private InputActionProperty aButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string mainMenuSceneName = "Menu";

    private bool isPaused = false;

    private void OnEnable()
    {
        aButton.action.performed += Pause;
    }

    void Start()
    {
        
    }



    private void Pause(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true); 
                Debug.Log("Paused");
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false); 
                Debug.Log("Unpaused");
                isPaused = false;
            }
        }
    }



    public void Continue()
    {
        Time.timeScale = 1;
        Debug.Log("continue");
        isPaused = false;
        
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(mainMenuSceneName); 
    }

}





    //private void Update()
    //{
    //    if (isPaused && Input.GetKeyDown(KeyCode.Escape)) // Предполагается, что вы используете клавишу Escape для выхода из паузы
    //    {
    //        Resume();
    //    }
    //}

   