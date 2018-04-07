using UnityEngine;

public class StartingShip : MonoBehaviour {

    public GameObject Ship;

    private void OnMouseDown()
    {
        if (!GameController.instance.GameRunning)
        {
            Instantiate(Ship, transform.position, transform.rotation);
            GameController.instance.BeginGame();
            Destroy(gameObject);
        }     
    }
}
