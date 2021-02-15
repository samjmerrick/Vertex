using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public string AudioType;
    private AudioSource audioSource;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(AudioType, 1);
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(AudioType, value);
    }
}
