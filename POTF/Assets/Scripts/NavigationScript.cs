﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene(Constants.MenuSceneName);
    }

    public void GoToHowToPlay()
    {
        //SceneManager.LoadScene(Constants.HowToPlayScene);
    }


    public void GoToCredits()
    {
        SceneManager.LoadScene(Constants.CreditsScene);
    }

    public void GoToMap()
    {
        SceneManager.LoadScene(Constants.MapSceneName);
    }

    public void GoToMission1()
    {
        SceneManager.LoadScene(Constants.Mission1SceneName);
    }


    public void Exit()
    {
        Application.Quit();
    }

}
