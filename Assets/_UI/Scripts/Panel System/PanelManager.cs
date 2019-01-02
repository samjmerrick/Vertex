using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;
    public Panel GameMenu;
    public Panel DeathMenu;
    public Panel ContinueMenu;
    public GameObject MissionsPrefab;

   
    private void OnEnable()
    {
        GameController.GameBegin += StartGame;
        GameController.GameEnd += EndGame;
        ShipTakeDamage.Death += Continue;
    }

    private void OnDisable()
    {
        GameController.GameBegin -= StartGame;
        GameController.GameEnd -= EndGame;
        ShipTakeDamage.Death -= Continue;
    }

    // Use this for initialization
    void Start ()
    {
        ShowMenu(CurrentPanel);
	}
     
    public void ShowMenu(Panel panel)
    {
        // If there is a current panel, set current panel isOpen to false
        if (CurrentPanel != null) CurrentPanel.SetActive(false);

        // Set the new panel to be CurrentPanel and run active function
        CurrentPanel = panel;
        CurrentPanel.SetActive(true);
    }

    void Continue()
    {
        // Need to set gameObject to active to run coroutine
        ContinueMenu.gameObject.SetActive(true);
        ContinueMenu.GetComponent<ContinuePanel>().StartCountDown();
    }

    void StartGame()
    {
        ShowMenu(GameMenu);
    }

    void EndGame()
    {
        // Count the total of ToComplete among missions, only show mission list if progress has been made
        int totalProgress = 0, totalCacheProgress = 0;

        foreach (Mission mission in Missions.GetMissions())
        {
            totalProgress += mission.progress;
            totalCacheProgress += mission.cacheProgress;
        }

        if (totalProgress != totalCacheProgress)
        {
            StartCoroutine(ShowMissionProgress()); // We've made progress, show missions
        }
        
        else
        {
            ShowMenu(DeathMenu); // No progress, go straight to death menu
        }
    }

    IEnumerator ShowMissionProgress()
    {
        CurrentPanel.SetActive(false);
        GameObject ml = Instantiate(MissionsPrefab, transform);

        yield return new WaitForSeconds(3);

        Destroy(ml.gameObject);
        ShowMenu(DeathMenu);
    }

}
