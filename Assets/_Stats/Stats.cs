
using System;
using UnityEngine;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public static Dictionary<string, int> gameStats = new Dictionary<string, int>();
    public static Dictionary<string, int> bestStats = new Dictionary<string, int>();

    void OnEnable()
    {
        GameController.GameBegin += BeginGame;
        GameController.GameEnd += EndGame;
        Enemy.Death += CountDestroys;
        Pickup.Got += CountPickups;
    }

    void OnDisable()
    {
        GameController.GameBegin -= BeginGame;
        GameController.GameEnd -= EndGame;
        Enemy.Death -= CountDestroys;
        Pickup.Got -= CountPickups;
    }

    void BeginGame()
    {
        gameStats.Clear();
        gameStats.Add("Destroyed", 0);
        gameStats.Add("Pickups", 0);
        gameStats.Add("Distance", 0);
    }

    void EndGame()
    {
        foreach (KeyValuePair<string, int> stat in gameStats)
        {
            if (bestStats.ContainsKey(stat.Key))
			{
				if (bestStats[stat.Key] < stat.Value)
                {
                    bestStats[stat.Key] = stat.Value;
                    UIStats.newBest.Add(stat.Key);                  
                }
            }
            else
            {
                bestStats.Add(stat.Key, stat.Value);
                UIStats.newBest.Add(stat.Key);
            }
        }
    }

    private void CountDestroys(string name, Vector3 pos)
    {
        gameStats["Destroyed"] += 1;

        UIControl.instance.Destroyed.text = gameStats["Destroyed"].ToString();
    }

    private void CountPickups(string name, int time)
    {
        gameStats["Pickups"] += 1;
    }
}