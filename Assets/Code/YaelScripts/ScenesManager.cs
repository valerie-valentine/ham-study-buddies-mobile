using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public enum Scene
    {
        StartPage,
        SignIn,
        PickAHamster,
        MainPage
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadMainPage()
    {
        SceneManager.LoadScene(Scene.MainPage.ToString());
        audioManager.SetCurrentSceneMusic(audioManager.gameBackground); // Change music for this scene
    }


    public void LoadStartPage()
    {
        SceneManager.LoadScene(Scene.StartPage.ToString());
        audioManager.SetCurrentSceneMusic(audioManager.startMenuBackground); // Change music for this scene
    }

    public void LoadSignInPage()
    {
        SceneManager.LoadScene(Scene.SignIn.ToString());
    }


    public void LoadPickAHamsterPage()
    {
        SceneManager.LoadScene(Scene.PickAHamster.ToString());
    }
}
