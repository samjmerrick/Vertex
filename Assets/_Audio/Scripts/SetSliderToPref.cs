using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetSliderToPref : MonoBehaviour
{  
    public string PlayerPrefName;

    void OnEnable()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(PlayerPrefName, 1);
    }

}
