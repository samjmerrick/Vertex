using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;
    private bool sequenceRunning;

    public delegate void PanelChange(Panel panel);
    public static event PanelChange ChangePanel;

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
}
