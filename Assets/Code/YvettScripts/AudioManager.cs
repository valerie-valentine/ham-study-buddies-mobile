using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    //public AudioClip nameOfSFX1;
    //public AudioClip nameOfSFX2;

    private void Start()
    {
        musicSource.clip= background;
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