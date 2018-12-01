using System;
using System.Collections;
using UnityEngine;

public class ShipTakeDamage : MonoBehaviour {

    private bool invincible = false;
    public int invincibleTime;
    public GameObject explosion;

    public static event Action Death;

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
        if (!enabled || invincible) return;

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

    public void GiveLife()
    {
        gameObject.SetActive(true);
        StartCoroutine(SetInvincible());
    }

    IEnumerator SetInvincible()
    {
        invincible = true;
        StartCoroutine(Flash());

        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }

    IEnumerator Flash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while (invincible)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(.1f);
        }
        sr.enabled = true;
    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}
