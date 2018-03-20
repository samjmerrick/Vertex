using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        // Set the bullet to destroy itself after 1 seconds
        Destroy(gameObject, 5.0f);

        // Push the bullet in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(300, 400));
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag.Equals("Bullet")) {
            // Destroy the current asteroid
            Destroy(gameObject);
            // Destroy the bullet
            Destroy(c.gameObject);

        }
    }
}