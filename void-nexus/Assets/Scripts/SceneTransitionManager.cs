using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public TMP_Dropdown dropdown;
    [SerializeField] private InputActionProperty aButton;
    [SerializeField] private bool isPressing = false;
    [SerializeField] private Image myBar;

    public float loadTime = 1f;
    public float loadTimer;

    private void OnEnable()
    {
        aButton.action.performed += nachalo;
        aButton.action.canceled += end;
    }

    public void GoToScene(int SceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(SceneIndex));
    }

    IEnumerator GoToSceneRoutine(int SceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(SceneIndex);
    }

    public void GoToSceneAsync(int SceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(SceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int SceneIndex)
    {
        fadeScreen.FadeOut();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneIndex);
        asyncOperation.allowSceneActivation = false;

        float timer = 0f;
        while(timer <= fadeScreen.fadeDuration && !asyncOperation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
    }

    private void Update()
    {
        if(dropdown.value == 1)
        {
            PlayerPrefs.SetInt("Movement", 1);
        }
        if (dropdown.value == 0)
        {
            PlayerPrefs.SetInt("Movement", 0);
        }

        if (isPressing)
        {
            loadTimer += Time.deltaTime;
            myBar.fillAmount = loadTimer / loadTime;
            if (loadTimer > loadTime)
            {
                isPressing = false;
                myBar.fillAmount = 0f;
                GoToSceneAsync(1);

                return;
            }
        }
        else
        {
            myBar.fillAmount = 0f;
            loadTimer = 0f;
        }
    }

    private void nachalo(InputAction.CallbackContext context)
    {
        isPressing = true;
        Debug.Log("true");
    }

    private void end(InputAction.CallbackContext context)
    {
        isPressing = false;
        Debug.Log("false");
    }
}
