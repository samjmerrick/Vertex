using UnityEngine;

public class StartingShip : MonoBehaviour {

    public PanelManager panelManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float diff = Vector2.Distance(mousePos, transform.position);

            if (diff < 2.5f)
                StartGame();
        }

        if(panelManager.CurrentPanel.name == "Start Menu" || panelManager.CurrentPanel.name == "Game Menu"){
            spriteRenderer.enabled = true;
        }
        else{
            spriteRenderer.enabled = false;
        }
            
    }

    void StartGame()
    {
        if (!GameController.GameRunning && panelManager.CurrentPanel.name == "Start Menu")
        {
            FindObjectOfType<GameController>().BeginGame();

            GetComponent<ShipMove>().enabled = true;
            enabled = false;
        }
    }
}
