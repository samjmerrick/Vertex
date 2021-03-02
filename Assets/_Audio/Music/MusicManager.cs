using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource GameMusic;

    public void OnEnable()
    {
        GameController.GameBegin += PlayMainMusic;
        GameController.GameEnd += StopMainMusic;
    }

    public void OnDisable()
    {
        GameController.GameBegin -= PlayMainMusic;
        GameController.GameEnd += StopMainMusic;
    }

    void PlayMainMusic()
    {
        GameMusic.volume = 0;
        GameMusic.Play();

        StartCoroutine(StartFade(GameMusic, duration: 0.5f, targetVolume: 1));
    }

    void StopMainMusic()
    {
        StartCoroutine(StartFade(GameMusic, duration: 0.5f, targetVolume: 0));
    }

    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        if(targetVolume == 0)
        {
            audioSource.Stop();
            audioSource.volume = 1;
        }
    }
}
