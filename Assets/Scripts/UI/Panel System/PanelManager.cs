using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;
    public Panel GameMenu;
    public Panel DeathMenu;

    public delegate void ChangePanelDelegate(Panel panel);
    public static event ChangePanelDelegate ChangePanel;

    private void OnEnable()
    {
        GameController.GameBegin += StartGame;
        GameController.GameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameController.GameBegin -= StartGame;
        GameController.GameEnd -= EndGame;
    }

    // Use this for initialization
    void Start ()
    {
        ShowMenu(CurrentPanel);
	}
     
    public void ShowMenu(Panel panel)
    {
        // If there is a current panel, set current panel isOpen to false
        if (CurrentPanel != null) CurrentPanel.isOpen = false;

        CurrentPanel = panel;

        CurrentPanel.isOpen = true;
        ChangePanel(CurrentPanel);

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
