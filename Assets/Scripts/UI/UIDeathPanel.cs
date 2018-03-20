using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeathPanel : MonoBehaviour {

	// Use this for initialization
	void OnEnable ()
    {

        Text bestText = transform.Find("Best").gameObject.GetComponent<Text>();
        int best = PlayerPrefs.GetInt("best");
        bestText.text = "Best: " + best;

        Text scoreText = transform.Find("Score").gameObject.GetComponent<Text>();
        scoreText.text = "Score: " + GameController.score;

        Text stats = transform.Find("Stats").gameObject.GetComponent<Text>();
        stats.text = "You killed " + GameController.instance.EnemiesKilled;
    }
}
