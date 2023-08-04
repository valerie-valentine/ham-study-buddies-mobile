using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
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
    }


    public void LoadStartPage()
    {
        SceneManager.LoadScene(Scene.StartPage.ToString());

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
