using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static event Action GameBegin;
    public static event Action GameEnd;

    public delegate void IntDelegate (int dist);
    public static event IntDelegate Distance;

    public static bool GameRunning = false;
    public bool isQuitting = false;

    void OnEnable()
    {
        Application.targetFrameRate = 60;
        SaveManager.Load();
    }

    void OnDisable()
    {
        SaveManager.Save();
    }

    public void BeginGame()
    {
        GameBegin();

        GameRunning = true;
        StartCoroutine(AddDistance());
    }

    public void EndGame()
    {
        GameRunning = false;
     
        if (!isQuitting)
        {
            CancelInvoke();
            GameEnd();
        }
    }

    IEnumerator AddDistance()
    {
        while (GameRunning)
        {
            Stats.gameStats["Distance"] += 1;

            if (Distance != null)
                Distance(1);

            UIControl.instance.Distance.text = Stats.gameStats["Distance"].ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DeleteHighScore()
    {
        Missions.LoadMissions(new List<Mission>()); // Loads 0 Missions
        Stats.bestStats.Clear();
        Upgrades.Reset();
        Coins.Set(10000);
        SaveManager.ClearSave();
		FacebookManager.LogOut();
		RestartScene ();
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