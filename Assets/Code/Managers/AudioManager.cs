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
}


// FIRST --- in other scripts, add this:
// AudioManager audioManager;

// SECOND --- inside private void Awake()
// {
//      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
// }

// THIRD --- go to where you want to call the function
// audioManager.PlaySFX(audioManager.nameOfSFX1);