using UnityEngine;

public class StartingShip : MonoBehaviour {

    public GameObject Ship;
    public PanelManager panelManager;

    private void OnMouseDown()
    {
        if (!GameController.instance.GameRunning && panelManager.CurrentPanel.name == "Start Menu")
        {
            Instantiate(Ship, transform.position, transform.rotation);
            GameController.instance.BeginGame();
            Destroy(gameObject);
        }     
    }
}
