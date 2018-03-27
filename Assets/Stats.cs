using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public GameObject stat;

    private void OnEnable()
    {
        if (GameController.gameStats != null)
            foreach (KeyValuePair<string, int> entry in GameController.gameStats)
            {
                GameObject go = Instantiate(stat, transform);
                go.transform.Find("Description").GetComponent<Text>().text = entry.Key;
                go.transform.Find("Score").GetComponent<Text>().text = entry.Value.ToString();
            }
    }
}
