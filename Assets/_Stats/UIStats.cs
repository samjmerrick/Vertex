using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour {

    public GameObject stat;
    public GameObject hiScoreBadge;
    public static List<string> newBest = new List<string>();

    private void OnEnable()
    {
        if (Stats.gameStats != null)
        {
            foreach (KeyValuePair<string, int> entry in Stats.gameStats)
            {
                GameObject go = Instantiate(stat, transform);
                go.transform.Find("Description").GetComponent<Text>().text = entry.Key;
                go.transform.Find("Score").GetComponent<Text>().text = entry.Value.ToString();
                go.transform.Find("Best").GetComponent<Text>().text = Stats.bestStats[entry.Key].ToString();

                if (newBest.Contains(entry.Key))
                {
                    //go.transform.Find("Best").GetComponent<Text>().color = new Color32(50, 205, 50, 255);
                    Instantiate(hiScoreBadge, go.transform);
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

