using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

// SOUND EFFECT RULES: when you unlock something with ur duduk - UNLOCKDUDUK
                    // when you pick up an inventory - PICKUPINVENTORY
                    // when you discover a new mechanic - DISCOVERNEWMECHANIC
                    
                    // when you walk - WALK


public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;
    private float audioValue;
    private float musicValue;

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null) return; //Debug.Log("music not found");
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) Debug.Log("Sound not found");
        else sfxSource.PlayOneShot(s.clip);
    }

    public void StopSFX()
    {
        if (sfxSource.isPlaying) sfxSource.Stop();
    }

// use the two methods below to change the volume of sfx or music
    public void SetSFXLevel(float value)
    {
        sfxSource.volume = value;
        audioValue = value;
    }

    public void SetMusicLevel(float value)
    {
        musicSource.volume = value;
        musicValue = value;
    }

// use the two methods below when you are changing scenes.
    public void FadeInMusic() 
    {
        musicSource.DOFade(musicValue, 1.0f);
    }

    public void FadeOutMusic(float time)
    {
        musicSource.DOFade(0f, time);
    }

    public void FadeInMusic(float time) 
    {
        musicSource.DOFade(musicValue, time);
    }

    public void FadeOutMusic()
    {
        musicSource.DOFade(0f, 1.0f);
    }

    public void ChangeMusic(int newScene)
    {
        switch (newScene)
        {
            case 6:
                //image.sprite = sprites[0];
                PlayMusic("Intro");
                break;
            case 7:
                PlayMusic("Home");
                break;
            case 9:
                PlayMusic("Bazaar");
                break;
            case 11:
                PlayMusic("WorkPlace");
                break;
            case 12: 
                PlayMusic("Park");
                PlaySFX("BirdsChirping");
                break;
            case 13:
                PlayMusic("OminousMusic");
                break;
            case 17:
                PlayMusic("CityCenter");
                break;
            case 21:
                PlayMusic("SecretSociety");
                break;
            case 23:
                PlayMusic("SultansPalace1");
                break;
            case 24:
                PlayMusic("OminousMusic");
                break;
            default:
                PlayMusic(null);
                break;
        }
    }
}
