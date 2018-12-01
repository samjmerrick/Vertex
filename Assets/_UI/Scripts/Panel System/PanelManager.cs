using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;
    public Panel GameMenu;
    public Panel DeathMenu;
    public Panel ContinueMenu;

   
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
        ShowMenu(DeathMenu);
    }
}
