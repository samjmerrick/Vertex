using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;

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
        if (CurrentPanel != null)
            CurrentPanel.isOpen = false;

        CurrentPanel = panel;

        if (CurrentPanel.ShowBeforeLoading.Length != 0)
        {
            StartCoroutine(ShowMenusInSequence(CurrentPanel.ShowBeforeLoading));
        }
        else
        {
            CurrentPanel.isOpen = true;
            ChangePanel(CurrentPanel);
        }
    }

    IEnumerator ShowMenusInSequence(Panel[] Panels)
    {
        foreach (Panel panel in Panels)
        {
            ShowMenu(panel);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
