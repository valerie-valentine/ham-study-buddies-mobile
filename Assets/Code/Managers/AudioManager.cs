using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip startMenuBackground;
    public AudioClip gameBackground;
    public AudioClip startButtonClickSFX;
    public AudioClip hamsterClickSFX;
    public AudioClip buttonClickSFX;
    public AudioClip buttonClick2SFX;
    public AudioClip timerCompletedClickSFX;
    public AudioClip animeSparklesSFX;
    public AudioClip animePowerUpSFX;
    public AudioClip animeYellSFX;
    public AudioClip eggCrackSFX;
    public AudioClip quitTimerSFX;
    public AudioClip deleteSFX;
    public AudioClip kaChingSFX;
    public AudioClip gojoSparklesSFX;
    public AudioClip hamsterWheelSFX;
    private AudioClip currentSceneMusic;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentSceneMusic = startMenuBackground;
        PlayCurrentSceneMusic(currentSceneMusic);
    }

    public void SetCurrentSceneMusic(AudioClip musicClip)
    {
        currentSceneMusic = musicClip;
        PlayCurrentSceneMusic(currentSceneMusic);
    }

    private void PlayCurrentSceneMusic(AudioClip currentSceneMusic)
    {
        musicSource.clip = currentSceneMusic;
        AudioListener.volume = 1;
        musicSource.Play();
    }

    // to be called by other scripts, plays SFX
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXLoop(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.loop = true;
        SFXSource.Play();
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }
}