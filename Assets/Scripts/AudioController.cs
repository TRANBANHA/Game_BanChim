using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioController : Singleton<AudioController>
{
    [Header("Main Setting:")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sfxVolume;

    public AudioSource musicAus;
    public AudioSource sfxAus;
    [Header("Game Sounds and Musics:")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] bgmusics;

    public override void Start()
    {
        PlayMusic(bgmusics);
    }


    public void PlaySound(AudioClip sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus=sfxAus;
        }
        if (aus)
        {
            aus.PlayOneShot(sound,sfxVolume);
        }
    }
    public void PlaySound(AudioClip[] sounds, AudioSource aus = null)
    {
        if (!aus)
        {
            aus=sfxAus;
        }
        if (aus)
        {
            int ranInx = Random.Range(0,sounds.Length);
            if (sounds[ranInx] != null)
            {
                aus.PlayOneShot(sounds[ranInx],sfxVolume);
            }
        }
    }
    public void PlayMusic(AudioClip music, bool loop=true)
    {
        if (musicAus)
        {
            musicAus.clip = music;
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }
    public void PlayMusic(AudioClip[] music, bool loop = true)
    {
        if(musicAus)
        {
            int ranInx= Random.Range(0,music.Length);

            if (music[ranInx] != null)
            {
                musicAus.clip = music[ranInx];  
                musicAus.loop = loop;   
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
        }
    }
}
