using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXStartScene : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayStartButtonClickSFX()
    {
        audioManager.PlaySFX(audioManager.startButtonClickSFX);
    }
}
