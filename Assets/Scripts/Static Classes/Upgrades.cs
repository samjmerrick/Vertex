using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Upgrades {

    public static Dictionary<string, int> active = new Dictionary<string, int>()
    {
        { "Laser", 0 },
        { "Rapid", 0 },
        { "Shield", 0},
        { "Triple", 0},
        { "Big", 0}
    };

    public static Dictionary<string, int> Get() { return new Dictionary<string, int>(active); }

    public static int Get(string key)
    {
        if (active.ContainsKey(key))
        {
            return active[key];
        }
        else
        {
            new KeyNotFoundException(key + " is not in the upgrade list");
            return 0;
        }
    }


    public static void Reset()
    {
        active.Clear();
    }

    public static void Amend(string key, int amount)
    {
        if (!active.ContainsKey(key))
        {
            active.Add(key, amount);
        }
        else
        {
            active[key] += amount;
        }
                   
        Debug.Log("Changed " + key + " to " + active[key]);
    }

    public static void LoadUpgrades(Dictionary<string, int> loadedUpgrades)
    {
        active = loadedUpgrades;
    }
    
}
