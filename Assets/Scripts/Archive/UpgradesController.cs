using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesController : MonoBehaviour {

	public Slider ShieldSlider;
	public Slider DoubleSlider;
	public Slider LaserSlider;
	public Slider TripleSlider;

	void Start () {
		ShieldSlider.value = PlayerPrefs.GetFloat("Shield", 1);
	}

	public void UpdateShield(int i){
		ShieldSlider.value += i;
		PlayerPrefs.SetFloat("Shield", ShieldSlider.value);
	}
}
