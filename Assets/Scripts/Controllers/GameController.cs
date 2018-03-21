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

    public GameObject background;

    public static int score;
    private float lastScored;
    private int combo;
    private float comboCount;

    private float lastComboDecrease;
    public int EnemiesDestroyed;

    public delegate void gameBegin();
    public static event gameBegin GameBegin;

    public delegate void gameEnd();
    public static event gameEnd GameEnd;

    public static bool GameRunning = false;
    public static bool isQuitting = false;

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
        GameRunning = true;

        GameObject canvas = GameObject.Find("Canvas");
        Panel deathMenu = canvas.transform.Find("Game Menu").GetComponent<Panel>();

        canvas.GetComponent<PanelManager>().ShowMenu(deathMenu);

        score = 0;
        EnemiesDestroyed = 0;
        combo = 1; 
        comboCount = 0;

        GameBegin();
    }

    public void EndGame()
    {
        GameRunning = false;

        if (!isQuitting)
        {
            GameObject canvas = GameObject.Find("Canvas");
            Panel deathMenu = canvas.transform.Find("Death Menu").GetComponent<Panel>();

            canvas.GetComponent<PanelManager>().ShowMenu(deathMenu);

            CancelInvoke();
            GameEnd();
        }
    
    }

    public void IncrementScore(int x)
    {
        lastScored = Time.time;
        score += x * combo;
        UIControl.instance.UpdateScore(score);

        if (comboCount >= 0 && comboCount <= 5) combo = 1;
        if (comboCount >= 5 && comboCount <= 10) combo = 2;
        if (comboCount >= 10) combo = 4;

        if (comboCount >= 10 && comboCount<= 15) comboCount += 0.2f;
        if (comboCount >= 5 && comboCount <= 10) comboCount += 0.4f;
        if (comboCount >= 0 && comboCount <= 5) comboCount += 0.5f;


        UIControl.Instance.SetCombo(comboCount);

        if (score % 50 == 0)
            SpawnController.toSpawn += 1;
    }

    private void Update()
    {
        if (lastComboDecrease <= Time.time)
        {
            if (Time.time - lastScored > 0.5f && comboCount >= 0)
            {
                comboCount -= 0.005f * comboCount;
                UIControl.Instance.SetCombo(comboCount);
            }
            if (comboCount >= 0 && comboCount <= 5) combo = 1;
            if (comboCount >= 5 && comboCount <= 10) combo = 2;
            if (comboCount >= 10) combo = 4;

            lastComboDecrease = Time.time;
        }  
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
        EnemiesDestroyed++;
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