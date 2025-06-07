using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.SearchService;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backgroundClip;
    public AudioClip ItemPickUpClip;
    public AudioClip ClickClip;
    public AudioClip WinClip;
    public AudioClip GameOverClip;
    public AudioClip EnemysfxClip;

    private void Start()
    {
        musicSource.clip = backgroundClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopPlaying()
    {
        musicSource.Stop();
    }

    public void StartPlaying(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}



