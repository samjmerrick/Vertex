using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetSliderValueFromPrefs : MonoBehaviour
{
    public string PlayerPref;
    public float defaultValue;

    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume", defaultValue);
    }
}