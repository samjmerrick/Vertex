using System.Collections.Generic;
using UnityEngine;

public class StatManager {

    Dictionary<string, int> stats;

    void Init()
    {
        if (stats != null)
            return;

        stats = new Dictionary<string, int>();
    }

    public int GetStat(string statType)
    {
        Init();

        if (!stats.ContainsKey(statType))
            return 0;

        return stats[statType];
    }

    public void SetStat(string statType, int value)
    {
        Init();
        if (!stats.ContainsKey(statType))
            stats.Add(statType, 0);

        stats[statType] = value;
    }

    public void ChangeStat(string statType, int amount)
    {
        Init();
        int currStat = GetStat(statType);
        SetStat(statType, currStat + amount);
    }
}
