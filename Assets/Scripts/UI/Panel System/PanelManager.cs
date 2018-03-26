using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    public static Panel CurrentPanel;

	// Use this for initialization
	void Start () {
        CurrentPanel = transform.Find("Start Menu").GetComponent<Panel>();
        ShowMenu(CurrentPanel);
	}
	
	public static void ShowMenu(Panel panel)
    {
        if (CurrentPanel != null)
        {
            CurrentPanel.isOpen = false;
            CurrentPanel.gameObject.SetActive(false);
        }

        CurrentPanel = panel;
        CurrentPanel.gameObject.SetActive(true);

        CurrentPanel.isOpen = true;
    }
}
