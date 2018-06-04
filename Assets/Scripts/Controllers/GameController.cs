using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PanelManager panelManager;
    public Canvas canvas;

    public delegate void gameBegin();
    public static event gameBegin GameBegin;

    public delegate void gameEnd();
    public static event gameEnd GameEnd;

    public delegate void distance(int dist);
    public static event distance Distance;

    public static bool GameRunning = false;
    public bool isQuitting = false;

    void OnEnable()
    {
        SaveManager.Load();
    }

    void OnDisable()
    {
        SaveManager.Save();
    }

    public void BeginGame()
    {
        GameBegin();

        Panel gameMenu = canvas.transform.Find("Game Menu").GetComponent<Panel>();
        panelManager.ShowMenu(gameMenu);


        GameRunning = true;
        StartCoroutine(AddDistance());
        
    }

    public void EndGame()
    {
        Panel deathMenu = canvas.transform.Find("Death Menu").GetComponent<Panel>();
        panelManager.ShowMenu(deathMenu);

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
        Upgrades.Reset();
        Coins.Set(10000);
        SaveManager.ClearSave();
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