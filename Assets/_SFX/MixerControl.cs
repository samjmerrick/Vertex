using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerControl : MonoBehaviour
{
    public AudioMixer mixer;

    private string musicPref = "Music";
    private string sfxPref = "SFX";

    private string musicMixerParam = "musicVol";
    private string sfxMixerParam = "sfxVol";

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(musicPref, 1);
        mixer.SetFloat(musicMixerParam, convertToLinear(savedMusicVolume));

        float savedSFXVolume = PlayerPrefs.GetFloat(sfxPref, 1);
        mixer.SetFloat(sfxMixerParam, convertToLinear(savedSFXVolume));
    }

    public void SetMusicLevel(float volume)
    {
        SetLevel(musicMixerParam, musicPref, volume);
    }

    public void SetSFXLevel(float volume)
    {
        SetLevel(sfxMixerParam, sfxPref, volume);
    }

    public float ReturnMusicLevel()
    {
        return PlayerPrefs.GetFloat(musicPref, 1);
    }

    public float ReturnSFXLevel()
    {
        return PlayerPrefs.GetFloat(sfxPref, 1);
    }

    private void SetLevel(string mixerParam, string playerPref, float volume)
    {
        // Save the value before conversion to linear
        PlayerPrefs.SetFloat(playerPref, volume);

        mixer.SetFloat(mixerParam, convertToLinear(volume));
    }

    private float convertToLinear(float value)
    {
        return Mathf.Log10(value) * 20;
    }

}
