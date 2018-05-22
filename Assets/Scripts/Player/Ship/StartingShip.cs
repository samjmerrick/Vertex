using UnityEngine;

public class StartingShip : MonoBehaviour {

    public PanelManager panelManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float diff = Vector2.Distance(mousePos, transform.position);

            if (diff < 2.5f)
                StartGame();

        }
    }

    void StartGame()
    {
        if (!GameController.instance.GameRunning && panelManager.CurrentPanel.name == "Start Menu")
        {
            GameController.instance.BeginGame();

            GetComponent<ShipMove>().enabled = true;
            enabled = false;
        }
    }
}
