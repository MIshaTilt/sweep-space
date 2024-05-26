using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Heat;

public class UIMenu : MonoBehaviour
{
    [SerializeField] ButtonManager play;

    void Start()
    {
        play.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        ScenesManager.Sample.LoadScene(ScenesManager.Scene.LastGrayboxDone);
        //ScenesManager.Sample.LoadScene(ScenesManager.Scene.Player);

    }

}   
