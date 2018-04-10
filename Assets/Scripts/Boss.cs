using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public Transform[] ShootPoints;

    public float ShootFrequency;
    public int ShootSpeed;
    public int clipSize;
    public int reloadTime;

    public float dodge;
    public float smoothing;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    private float targetManeuver;

    private int numberShot;
    private float currentSpeed;
    private bool isMoving = true;
    private Rigidbody2D rb;

    public GameObject bullet;

    private void Start()
    {
        StartCoroutine(Shoot());

        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (!isMoving)
            {
                foreach (Transform transform in ShootPoints)
                {
                    GameObject go = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
                    go.GetComponent<Rigidbody2D>().AddForce(transform.up * -ShootSpeed);
                }
                numberShot++;

                yield return new WaitForSeconds(ShootFrequency);

                if (numberShot == clipSize)
                {
                    StartCoroutine(Evade());
                    yield return new WaitForSeconds(reloadTime);
                    numberShot = 0;
                }
            }
            else
            {
                // Check again in .2 seconds
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    IEnumerator Evade()
    {
        targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
        yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        targetManeuver = 0;
        yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, currentSpeed, 0.0f);

        if (transform.position.y > 3f)
        {
            rb.velocity = transform.up * -Speed;
        }
        else
        {
            isMoving = false;
        }
            
    }
}
