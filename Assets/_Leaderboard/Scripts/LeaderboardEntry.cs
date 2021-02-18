using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public string id;
    public Text Rank;
    public Text DisplayName;
    public Text Score;

    public void Init(string id, int rank, string displayName, string score)
    {
        Rank.text = rank + ".";
        Score.text = score.ToString();
        DisplayName.text = displayName;
    }
}