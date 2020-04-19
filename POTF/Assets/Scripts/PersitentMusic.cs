using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersitentMusic : MonoBehaviour
{
    [SerializeField()]
    AudioSource MenuMusic;
    [SerializeField()]
    AudioSource MapMusic;

    //TODO: many random mission songs?
    [SerializeField()]
    AudioSource MissionMusic1;

    static PersitentMusic instance;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenu()
    {
        MapMusic.Stop();
        MissionMusic1.Stop();
        if (MenuMusic.isPlaying) return;
        MenuMusic.Play();
    }


    public void PlayMapMusic()
    {
        MissionMusic1.Stop();
        MenuMusic.Stop();
        if (MapMusic.isPlaying) return;
        MapMusic.Play();
    }

    public void PlayMissionMusic1()
    {
        MapMusic.Stop();
        MenuMusic.Stop();
        if (MissionMusic1.isPlaying) return;
        MissionMusic1.Play();
    }

    public void StopMusic()
    {
        MapMusic.Stop();
        MenuMusic.Stop();
        MissionMusic1.Stop();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case Constants.MenuSceneName:
                PlayMenu();
                break;
            case Constants.MapSceneName:
                PlayMapMusic();
                break;
            case Constants.Mission1SceneName:
                PlayMissionMusic1();
                break;
            default:
                break;
        }
    }
}