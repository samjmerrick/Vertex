using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerControl : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;

    private string musicPref = "Music";
    private string sfxPref = "SFX";

    private string musicMixerParam = "musicVol";
    private string sfxMixerParam = "sfxVol";

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(musicPref, 1);
        MusicMixer.SetFloat(musicMixerParam, convertToLinear(savedMusicVolume));

        float savedSFXVolume = PlayerPrefs.GetFloat(sfxPref, 1);
        SFXMixer.SetFloat(sfxMixerParam, convertToLinear(savedSFXVolume));
    }

    public void SetMusicLevel(float volume)
    {
        // Save the value before conversion to linear
        PlayerPrefs.SetFloat(musicPref, volume);
        MusicMixer.SetFloat(musicMixerParam, convertToLinear(volume));
    }

    public void SetSFXLevel(float volume)
    {
        // Save the value before conversion to linear
        PlayerPrefs.SetFloat(sfxPref, volume);
        SFXMixer.SetFloat(sfxMixerParam, convertToLinear(volume));
    }

    public float ReturnMusicLevel()
    {
        return PlayerPrefs.GetFloat(musicPref, 1);
    }

    public float ReturnSFXLevel()
    {
        return PlayerPrefs.GetFloat(sfxPref, 1);
    }

    private float convertToLinear(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
