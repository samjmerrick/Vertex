using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth = 1;


    private bool collided = false;


    public void DecreaseHealth()
    {
        maxHealth -= 1;
        GameController.IncrementScore(1);



        if (maxHealth <= 0 && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }

}
