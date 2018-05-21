using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTakeDamage : MonoBehaviour {

    public GameObject explosion;

    public delegate void Died();
    public static event Died Death;

    private void OnEnable()
    {
        GameController.GameEnd += Destroy;
    }

    private void OnDisable()
    {
        GameController.GameEnd -= Destroy;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy" || c.gameObject.tag == "EnemyFire")
        {
            if (explosion != null)
                Instantiate(explosion, transform.position, transform.rotation);

            if (Death != null)
                Death();

            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerFire"))
                Destroy(bullet);

            gameObject.SetActive(false);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
