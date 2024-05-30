using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBarFill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }



    public void LoadScene(int SceneId)
    {
        StartCoroutine(LoadSceneAsync(SceneId));
    }

    IEnumerator LoadSceneAsync(int SceneId)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }

    }
}
