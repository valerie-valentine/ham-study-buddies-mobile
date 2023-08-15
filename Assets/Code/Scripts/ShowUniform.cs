using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUniform : MonoBehaviour
{
    public GameObject objectToShow;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ShowAndHideObject()
    {
        StartCoroutine(ShowObjectCoroutine());
    }

    private IEnumerator ShowObjectCoroutine()
    {
        objectToShow.SetActive(true);
        audioManager.PlaySFX(audioManager.animeSparklesSFX);
        yield return new WaitForSeconds(1.3f); // Show the object for 1 second

        objectToShow.SetActive(false);
    }
}

