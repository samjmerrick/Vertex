using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text Rank;
    public Text DisplayName;
    public Text Score;

    public void Init(int rank, string displayName, string score)
    {
        Rank.text = rank + ".";
        Score.text = score.ToString();
        DisplayName.text = displayName;
    }
}