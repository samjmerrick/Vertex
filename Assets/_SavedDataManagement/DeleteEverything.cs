using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteEverything : MonoBehaviour
{
    public void Delete()
    {
        Missions.LoadMissions(new List<Mission>()); // Loads 0 Missions
        Stats.bestStats.Clear();
        Upgrades.Reset();
        Coins.Set(10000);
        SaveManager.ClearSave();
        PlayerPrefs.DeleteAll();
        DatabaseManager.RemoveScoreFromDatabase();        

        SceneManager.LoadScene(0);
    }
}
