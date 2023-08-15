using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    // public GameObject loadingScreen;
    public AudioManager audioManager;

    public float minLoadingDuration = 60f;
    public int sceneId;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        StartCoroutine(StartLoadingWithDelay(sceneId));
    }
    
    // public void LoadHamScene(int sceneId)
    // {
    //     StartCoroutine(StartLoadingWithDelay(sceneId));
    // }

    IEnumerator StartLoadingWithDelay(int sceneId)
    {
        // loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    // public void hideLoadScreen()
    // {
    //     loadingScreen.SetActive(false);
    // }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        audioManager.StopSFX();
        audioManager.SetCurrentSceneMusic(audioManager.gameBackground);

        while (!operation.isDone)
        {
            yield return null;
        }



        
        
    }
}
