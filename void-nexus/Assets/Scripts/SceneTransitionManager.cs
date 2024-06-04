using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public TMP_Dropdown dropdown;

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
    }
}
