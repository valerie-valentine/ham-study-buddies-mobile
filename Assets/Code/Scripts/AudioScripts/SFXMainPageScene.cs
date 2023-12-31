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

    public void PlayButtonClick2SFX()
    {
        audioManager.PlaySFX(audioManager.buttonClick2SFX);
    }

    public void PlayTimerCompletedClickSFX()
    {
        audioManager.PlaySFX(audioManager.timerCompletedClickSFX);
    }

    public void PlayQuitTimerSFX()
    {
        audioManager.PlaySFX(audioManager.quitTimerSFX);
    }

    public void PlayDeleteSFX()
    {
        audioManager.PlaySFX(audioManager.deleteSFX);
    }

    public void PlayKaChingSFX()
    {
        audioManager.PlaySFX(audioManager.kaChingSFX);
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

    public void PlayCookieVoice()
    {
        audioManager.PlaySFX(audioManager.curiousBabyDialogueSFX);
    }

    public void PlayDiscoBoogie()
    {
        audioManager.PlaySFX(audioManager.discoBallSFX);
    }

    public void PlayBaileyVoice()
    {
        audioManager.PlaySFX(audioManager.yahDialogueSFX);
    }

    public void PlayArbyVoice()
    {
        audioManager.PlaySFX(audioManager.happySqueelDialogueSFX);
    }

    public void PlayBingusVoice()
    {
        audioManager.PlaySFX(audioManager.funnySnarlDialogueSFX);
    }
}
