using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShip : Enemy
{
    public int ShootFrequency;
    public int ShootSpeed;
    public GameObject bullet;

    public float dodge;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody2D rb;

    void Start()
    {
        // Spawn formation - WORK ON THIS
        // if (Random.value < 0.5)
        //     Instantiate(this, new Vector3(-transform.position.x, transform.position.y), transform.rotation);

        InvokeRepeating("Shoot", ShootFrequency, ShootFrequency);
        rb = GetComponent<Rigidbody2D>();

        Speed += Random.Range(0f, 1.5f);

        //push in facing direction
        rb.velocity = transform.up * Speed * -1;
    
        currentSpeed = rb.velocity.y;
        StartCoroutine(Evade());
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 0.5f), Quaternion.Euler(0,0,180));
        go.GetComponent<Rigidbody2D>().AddForce(transform.up * -ShootSpeed);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }
    
    void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3 (newManeuver, currentSpeed, 0.0f);

        Collider2D[] cc = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        if (cc.Length >= 2)
        {
            // do something here
        }
    }
}
