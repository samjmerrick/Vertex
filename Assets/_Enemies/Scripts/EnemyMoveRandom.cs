using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour {

    public float Speed;

	void Start ()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        rb.AddForce(transform.up * Speed);
	}
}
