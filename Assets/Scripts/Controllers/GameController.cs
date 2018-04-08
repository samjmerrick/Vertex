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

    public PanelManager panelManager;
    public Canvas canvas;

    public int bestScore;

    public delegate void gameBegin();
    public static event gameBegin GameBegin;

    public delegate void gameEnd();
    public static event gameEnd GameEnd;

    public bool GameRunning = false;
    public bool isQuitting = false;

    void OnEnable()
    {
        SaveManager.Load();
        Ship.Death += EndGame;
        Enemy.Death += CountDestroys;
    }

    void OnDisable()
    {
        SaveManager.Save();
        Ship.Death -= EndGame;
        Enemy.Death -= CountDestroys;
    }

    public void BeginGame()
    {
        gameStats.Clear();
        gameStats.Add("Score", 0);
        gameStats.Add("Destroyed", 0);
        gameStats.Add("Time Elapsed", (int)Time.time);

        bestScore = PlayerPrefs.GetInt("Best");
        Panel gameMenu = canvas.transform.Find("Game Menu").GetComponent<Panel>();
        panelManager.ShowMenu(gameMenu);

        GameRunning = true;
        GameBegin();
    }

    public void EndGame()
    {
        GameRunning = false;
        gameStats["Time Elapsed"] = (int)Time.time - gameStats["Time Elapsed"];

        if (!isQuitting)
        {
            Panel deathMenu = canvas.transform.Find("Death Menu").GetComponent<Panel>();
            panelManager.ShowMenu(deathMenu);

            CancelInvoke();
            GameEnd();
        }
    }

    public void IncrementScore(int x)
    {
        gameStats["Score"] += x;

        int score = (int)gameStats["Score"];

        UIControl.instance.UpdateScore(score);

        if (score % 25 == 0)
            SpawnController.toSpawn += 1;

        if (score > bestScore)
            PlayerPrefs.SetInt("Best", score);
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteAll();
        Mission.LoadMissions(new List<Mission>()); // Loads 0 Missions
        Ship.upgrades.Clear();
        PlayerPrefs.SetInt("Coins", 10000);
    }

    private void CountDestroys(string name, Vector3 pos)
    {
        gameStats["Destroyed"] += 1;
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