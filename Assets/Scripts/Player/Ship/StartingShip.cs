using UnityEngine;

public class StartingShip : MonoBehaviour {

    public PanelManager panelManager;

    private void OnMouseDown()
    {
        if (!GameController.instance.GameRunning && panelManager.CurrentPanel.name == "Start Menu")
        {
            GameController.instance.BeginGame();

            GetComponent<ShipMove>().enabled = true;
            enabled = false;
        }     
    }
}
