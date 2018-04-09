using UnityEngine;

public class PanelManager : MonoBehaviour {

    public Panel CurrentPanel;

	// Use this for initialization
	void Start ()
    {
        ShowMenu(CurrentPanel);
	}

    public void ShowMenu(Panel panel)
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
