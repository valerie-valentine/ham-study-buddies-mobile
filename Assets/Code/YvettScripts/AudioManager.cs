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
}
