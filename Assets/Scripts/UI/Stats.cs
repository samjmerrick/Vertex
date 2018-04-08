using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public GameObject stat;
    public static List<string> newBest = new List<string>();

    private void OnEnable()
    {
        if (GameController.gameStats != null)
        {
            foreach (KeyValuePair<string, int> entry in GameController.gameStats)
            {
                GameObject go = Instantiate(stat, transform);
                go.transform.Find("Description").GetComponent<Text>().text = entry.Key;
                go.transform.Find("Score").GetComponent<Text>().text = entry.Value.ToString();
                go.transform.Find("Best").GetComponent<Text>().text = GameController.bestStats[entry.Key].ToString();

                if (newBest.Contains(entry.Key))
                {
                    go.transform.Find("Best").GetComponent<Text>().color = new Color(255, 0, 0);
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "Header")
                Destroy(child.gameObject);
        }
        newBest.Clear();
            
    }
}
