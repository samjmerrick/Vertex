using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public StatManager statmanager;
    public PanelManager panelManager;

    public static int score;

    public float timeElapsed;

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
        statmanager = new StatManager();
   
        GameObject canvas = GameObject.Find("Canvas");
        Panel gameMenu = canvas.transform.Find("Game Menu").GetComponent<Panel>();
        panelManager.ShowMenu(gameMenu);

        score = 0;

        timeElapsed = Time.time;

        GameRunning = true;
        GameBegin();
    }

    public void EndGame()
    {
        GameRunning = false;
        timeElapsed = Time.time - timeElapsed;

        if (!isQuitting)
        {
            GameObject canvas = GameObject.Find("Canvas");
            Panel deathMenu = canvas.transform.Find("Death Menu").GetComponent<Panel>();
            panelManager.ShowMenu(deathMenu);

            CancelInvoke();
            GameEnd();
        }
    }

    public void IncrementScore(int x)
    {
        score += x;
        UIControl.instance.UpdateScore(score);

        if (score % 50 == 0)
            SpawnController.toSpawn += 1;
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteAll();
        Mission.LoadMissions(new List<Mission>()); // Loads 0 Missions
        Ship.stats.Clear();
        PlayerPrefs.SetInt("Coins", 10000);
    }

    private void CountDestroys(string name, Vector3 pos)
    {
        statmanager.ChangeStat("Destroyed", 1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}