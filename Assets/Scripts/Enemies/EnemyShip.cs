using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy {

    private void Start()
    {

        float x = Random.Range(0, 2) == 0 ? -3 : 3;
        float y = Random.Range(6f, -1f);

        transform.position = new Vector3(x, y);

        // Find the Player
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;

        // Rotate to the Player
        Vector3 difference = Player.position + (new Vector3(0, -1)) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

        //push in facing direction
        GetComponent<Rigidbody2D>().AddForce(transform.up * Speed * -100);
    }


}
