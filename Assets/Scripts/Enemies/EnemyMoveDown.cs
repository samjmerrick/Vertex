using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDown : MonoBehaviour {

    public float Speed;
    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();

        Speed = Random.Range(Speed * 0.6f, Speed * 1.6f) * 20;

        rb.AddForce(-transform.up * Speed);
	}

}
