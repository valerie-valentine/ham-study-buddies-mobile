using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLoadWheelScene : MonoBehaviour
{
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //audioManager.PlaySFX(audioManager.hamsterWheelSFX);
        PlayHamsterSFX();
    }

    public void PlayHamsterSFX()
    {
        audioManager.PlaySFXLoop(audioManager.hamsterWheelSFX);
    }
}
