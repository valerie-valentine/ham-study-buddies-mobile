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
    public AudioClip timerCompletedClickSFX;
    public AudioClip animeSparklesSFX;
    public AudioClip animePowerUpSFX;
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
            Debug.Log("Audio Manager has been destroyed");
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

    // to be called by OnClick
    public void PlayStartButtonClickSFX()
    {
        SFXSource.PlayOneShot(startButtonClickSFX);
    }

    public void PlayHamsterClickSFX()
    {
        SFXSource.PlayOneShot(hamsterClickSFX);
    }

    public void PlayButtonClickSFX()
    {
        SFXSource.PlayOneShot(buttonClickSFX);
    }

    public void PlayTimerCompletedClickSFX()
    {
        SFXSource.PlayOneShot(timerCompletedClickSFX);
    }

    public void PlayAnimeSparklesSFX()
    {
        SFXSource.PlayOneShot(animeSparklesSFX);
    }

    public void PlayAnimePowerUpSFX()
    {
        SFXSource.PlayOneShot(animePowerUpSFX);
    }
}


// FIRST --- in other scripts, add this:
// AudioManager audioManager;

// SECOND --- inside private void Awake()
// {
//      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
// }

// THIRD --- go to where you want to call the function
// audioManager.PlaySFX(audioManager.nameOfSFX1);