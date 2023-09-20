using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        // Suscribe una función al evento que se dispara cuando el video termina.
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Cambia a la siguiente escena cuando el video termine.
        AudioManager.instance.musicSource.mute = false;
        SceneManager.LoadScene("InterfazInicial");
    }
}
