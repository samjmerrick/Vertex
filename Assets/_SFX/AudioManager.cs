using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioSource Source;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip Clip)
    {
        Source.clip = Clip;
        Source.Play();
    }
}
