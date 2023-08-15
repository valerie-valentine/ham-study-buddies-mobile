using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMainPageScene : MonoBehaviour
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

    public void PlayTimerCompletedClickSFX()
    {
        audioManager.PlaySFX(audioManager.timerCompletedClickSFX);
    }

    public void PlayAnimeSparklesSFX()
    {
        audioManager.PlaySFX(audioManager.animeSparklesSFX);
    }

    public void PlayAnimePowerUpSFX()
    {
        audioManager.PlaySFX(audioManager.animePowerUpSFX);
    }

    public void PlayAnimeYellSFX()
    {
        audioManager.PlaySFX(audioManager.animeYellSFX);
    }

    public void PlaySaiyanSFX()
    {
        audioManager.PlaySFX(audioManager.animeYellSFX);
        audioManager.PlaySFX(audioManager.animePowerUpSFX);
    }
}
