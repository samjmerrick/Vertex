using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameController instance = null;
    public static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();

                if (_instance == null)
                {
                    Debug.Log("no instance");
                }
            }

            return _instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    #endregion

    public static Dictionary<string, int> gameStats = new Dictionary<string, int>();
    public static Dictionary<string, int> bestStats = new Dictionary<string, int>();

    public PanelManager panelManager;
    public Canvas canvas;

    public delegate void gameBegin();
    public static event gameBegin GameBegin;

    public delegate void gameEnd();
    public static event gameEnd GameEnd;

    public delegate void distance(int dist);
    public static event distance Distance;

    public bool GameRunning = false;
    public bool isQuitting = false;

    void OnEnable()
    {
        SaveManager.Load();
        Ship.Death += EndGame;
        Enemy.Death += CountDestroys;
        Pickup.Got += CountPickups;
    }

    void OnDisable()
    {
        SaveManager.Save();
        Ship.Death -= EndGame;
        Enemy.Death -= CountDestroys;
        Pickup.Got -= CountPickups;
    }

    public void BeginGame()
    {
        gameStats.Clear();
        gameStats.Add("Destroyed", 0);
        gameStats.Add("Bosses", 0);
        gameStats.Add("Pickups", 0);
        gameStats.Add("Time Elapsed", (int)Time.time);
        gameStats.Add("Distance", 0);

        Panel gameMenu = canvas.transform.Find("Game Menu").GetComponent<Panel>();
        panelManager.ShowMenu(gameMenu);

        GameRunning = true;
        StartCoroutine(AddDistance());
        GameBegin();
    }

    public void EndGame()
    {
        GameRunning = false;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + gameStats["Destroyed"]);
        gameStats["Time Elapsed"] = (int)Time.time - gameStats["Time Elapsed"];

        foreach (KeyValuePair<string, int> stat in gameStats)
        {
            if (bestStats.ContainsKey(stat.Key))
            {
                if (bestStats[stat.Key] < stat.Value)
                {
                    bestStats[stat.Key] = stat.Value;
                    Stats.newBest.Add(stat.Key);
                }
            }
            else
            {
                bestStats.Add(stat.Key, stat.Value);
                Stats.newBest.Add(stat.Key);
            }
        }
            
        if (!isQuitting)
        {
            Panel deathMenu = canvas.transform.Find("Death Menu").GetComponent<Panel>();
            panelManager.ShowMenu(deathMenu);

            CancelInvoke();
            GameEnd();
        }
    }

    IEnumerator AddDistance()
    {
        while (GameRunning)
        {
            gameStats["Distance"] += 1;

            if (Distance != null)
                Distance(1);

            UIControl.instance.Distance.text = gameStats["Distance"].ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteAll();
        Mission.LoadMissions(new List<Mission>()); // Loads 0 Missions
        Ship.upgrades.Clear();
        PlayerPrefs.SetInt("Coins", 10000);
        SaveManager.ClearSave();
    }

    private void CountDestroys(string name, Vector3 pos)
    {
        if (name.Contains("Boss"))
        {
            gameStats["Bosses"] += 1;
        }

        else
        {
            gameStats["Destroyed"] += 1;
        }
        
        int destroyed = gameStats["Destroyed"];

        UIControl.instance.Destroyed.text = destroyed.ToString();

        if (destroyed % 25 == 0)
            GetComponent<SpawnController>().toSpawn++;
    }

    private void CountPickups(string name, int time)
    {
        gameStats["Pickups"] += 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}