using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSignInScene : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayButtonClickSFX()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX);
    }
}
