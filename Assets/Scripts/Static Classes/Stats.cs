
using UnityEngine;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public static Dictionary<string, int> gameStats = new Dictionary<string, int>();
    public static Dictionary<string, object> bestStats = new Dictionary<string, object>();

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
        gameStats.Add("Bosses", 0);
        gameStats.Add("Pickups", 0);
        gameStats.Add("Distance", 0);
    }

    void EndGame()
    {
        foreach (KeyValuePair<string, int> stat in gameStats)
        {
            if (bestStats.ContainsKey(stat.Key))
            {
                if ((int)bestStats[stat.Key] < stat.Value)
                {
                    bestStats[stat.Key] = stat.Value;
                    UIStats.newBest.Add(stat.Key);

                    if (stat.Key == "Destroyed")
                        FirebaseDatabaseController.WriteNewHiScore(stat.Value);
                }
            }
            else
            {
                bestStats.Add(stat.Key, stat.Value);
                UIStats.newBest.Add(stat.Key);
            }
        }

        FirebaseDatabaseController.SaveToDatabase("best-stats", bestStats);

        Coins.Add(gameStats["Destroyed"]);
    }

    private void CountDestroys(string name, Vector3 pos)
    {
        if (name.Contains("Boss"))
            gameStats["Bosses"] += 1;

        else
            gameStats["Destroyed"] += 1;

        UIControl.instance.Destroyed.text = gameStats["Destroyed"].ToString();

        if (gameStats["Destroyed"] % 25 == 0)
            GetComponent<SpawnController>().toSpawn++;
    }

    private void CountPickups(string name, int time)
    {
        gameStats["Pickups"] += 1;
    }
}