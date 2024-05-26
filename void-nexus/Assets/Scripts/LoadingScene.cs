using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //public void Loading(string sceneName)
    //{
    //    SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    //}

    //public string newSceneName = "lastGrayboxDone";
    //public GameObject playerPrefab; // ������ ������, ������� ����� ������������

    //public void SwitchScene()
    //{
    //    // ������� � ������� ��� ������� EventSystem �� ������� �����
    //    GameObject[] eventSystems = GameObject.FindGameObjectsWithTag("EventSystem");
    //    foreach (GameObject eventSystem in eventSystems)
    //    {
    //        Destroy(eventSystem);
    //    }

    //    // ��������� ����� �����
    //    SceneManager.LoadScene(newSceneName);

    //    //SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);

    //    // ���������� ������ ������ ����� �������� �����
    //    //Instantiate(playerPrefab);

    //}

    public static ScenesManager Sample;

    public void Awake()
    {
        Sample = this;
    }

    public enum Scene
    {
        UI,
        LastGrayboxDone,
        Player
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.LastGrayboxDone.ToString());
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(Scene.UI.ToString()); 
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPlayer()
    {
        SceneManager.LoadScene(Scene.Player.ToString());
    }
}
