using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSaiyin : MonoBehaviour

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
        audioManager.PlaySFX(audioManager.animePowerUpSFX);
        audioManager.PlaySFX(audioManager.animeYellSFX);
        yield return new WaitForSeconds(1.0f); // Show the object for 1 second

        objectToShow.SetActive(false);
    }
}

