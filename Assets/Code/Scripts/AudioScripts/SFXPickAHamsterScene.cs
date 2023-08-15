using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPickAHamsterScene : MonoBehaviour
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

    public void PlayHamsterClickSFX()
    {
        audioManager.PlaySFX(audioManager.hamsterClickSFX);
    }

    public void PlayEggCrackSFX()
    {
        audioManager.PlaySFX(audioManager.eggCrackSFX);
    }
}
