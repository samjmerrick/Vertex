using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy {

    public int followingShips;
    public float spacing;

    public EnemyMove[] movement;

    private float speed;
    private float rotSpeed;
    private Rigidbody2D rb;

    private Vector2 target;
    private int mov = 0;

	void OnEnable ()
    {
        // if this is the first ship, spawn more
        if (transform.parent == null)
        {
            for (int i = 0; i < followingShips; i++)
            {
                GameObject go = Instantiate(this.gameObject,
                            transform.position + new Vector3(0, (i + 1) * spacing),
                            transform.rotation,
                            transform);

                go.transform.parent = null;
            }          
        }
	}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Move();
    }

    private void FixedUpdate()
    {
        Vector2 direction = target - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotSpeed;

        rb.velocity = transform.up * speed;

        //transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.5f)
        {
            Move();
        }
    }

    void Move()
    {
        rotSpeed = movement[mov].rotSpeed;
        speed = movement[mov].Speed;
        target = movement[mov].waypoint.position;

        if (mov + 1 < movement.Length)
        {
            mov++;
        }
        else
        {
            mov = 0;
        }    
    }

    private void OnBecameInvisible()
    {
        // Override Enemy class (Do nothing).
    }
}


