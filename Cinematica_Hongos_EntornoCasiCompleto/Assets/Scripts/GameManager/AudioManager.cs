using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Soundss[] musicSounds;
    public Soundss[] sfxSound;
    public AudioSource musicSource, sfxSource;

    private float currentMusicVolume = 0.033f; // Volumen actual de la música

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicSource.volume = currentMusicVolume;
        sfxSource.volume = 0.2f;
    }

    public void PlayMusic(string name)
    {
        Soundss s = Array.Find(musicSounds, x => x.nameSound == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            StartCoroutine(CrossfadeMusic(s.clip)); // Inicia la transición gradual
        }
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip, float fadeDuration = 1.0f)
    {
        float startVolume = musicSource.volume;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0.0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = 0.0f;
        musicSource.Stop();

        musicSource.clip = newClip;
        musicSource.Play();

        while (elapsedTime < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(0.0f, currentMusicVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = currentMusicVolume;
    }

    public void playSfx(string name)
    {
        Soundss s = Array.Find(sfxSound, x => x.nameSound == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        currentMusicVolume = volume; // Actualiza el volumen actual de la música
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

[System.Serializable]
public class Soundss
{
    public string nameSound;
    public AudioClip clip;
}
