using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
