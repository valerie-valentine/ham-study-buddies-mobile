using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip startMenuBackground;
    public AudioClip gameBackground;
    //public AudioClip nameOfSFX1;
    //public AudioClip nameOfSFX2;
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
        //musicSource.clip = startMenuBackground;
        //musicSource.Play();
        currentSceneMusic = startMenuBackground; // Set initial music
        PlayCurrentSceneMusic();
    }

    public void SetCurrentSceneMusic(AudioClip musicClip)
    {
        currentSceneMusic = musicClip;
        PlayCurrentSceneMusic();
    }

    private void PlayCurrentSceneMusic()
    {
        musicSource.clip = currentSceneMusic;
        musicSource.Play();
    }

    // to be called by other scripts, plays SFX
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // toggle music
    public void ToggleMusic()
    {
        // if (click once, stop), if click again, continue?
        musicSource.clip = startMenuBackground;
        musicSource.Stop();
        Debug.Log("Audio has been toggled off");
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