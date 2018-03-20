using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUpdate : MonoBehaviour {

    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void OnGUI()
    {
        text.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
