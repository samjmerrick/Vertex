using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDown : MonoBehaviour {

    public float Speed;
    public bool RandomSpeed = true;

    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        if(RandomSpeed)
        {
            Speed = Random.Range(Speed * 0.8f, Speed * 1.2f);
        }

        Speed *= 20;
        

        rb.AddForce(-transform.up * Speed);
	}

}
