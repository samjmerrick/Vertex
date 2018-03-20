using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameBegin += BeginGame;
    }

    private void OnDisable()
    {
        GameController.GameBegin -= BeginGame;
    }

    // Use this for initialization
    void Awake ()
    {
        GetComponent<Ship>().enabled = false;
	}

    void BeginGame()
    {
        GetComponent<Ship>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (!GameController.GameRunning)
        {
            GameController.BeginGame();
            enabled = false;
        }
           
    }




}
