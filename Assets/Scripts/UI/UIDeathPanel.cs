using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeathPanel : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Text bestText = transform.Find("Best").gameObject.GetComponent<Text>();
        int best = PlayerPrefs.GetInt("Best");
        bestText.text = "Best: " + best;

        Text scoreText = transform.Find("Score").gameObject.GetComponent<Text>();
        scoreText.text = "Score: " + GameController.instance.score;

        Text stats = transform.Find("Stats").gameObject.GetComponent<Text>();

        if (GameController.instance != null)
            stats.text = "You destroyed " + GameController.instance.gameStats.GetStat("Destroyed") + " Enemies \n" +
                         "You lasted " + (int)GameController.instance.timeElapsed + " Seconds";
    }
}
