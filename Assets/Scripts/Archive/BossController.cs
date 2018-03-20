using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public GameObject bullet;

    private Vector3 pos;
    private float speed = 4;

    private Color currentColor;
    private Color toColor;
    private float colorStep;

    private bool collided = false;
    private bool moving = false;
    private float health;

    public delegate void BossDied();
    public static event BossDied bossDied;

    // Use this for initialization
    void Start()
    {
        health = 100;

        // x is -5 or 5
        var xAxis = Random.Range(0, 2) == 0 ? -5 : 5;

        transform.position = transform.position + new Vector3(xAxis, 3);

        colorStep = (1 / health);

        InvokeRepeating("ShootBullet", 1, 1);
        InvokeRepeating("Movement", 0, 2);
    }

    void OnEnable()
    {
    
    }

    void OnDisable()
    {

    }

    void Update()
    {
         float step = speed * Time.deltaTime;

        // Check if anything near. Set pos to be current location if so
        Collider2D[] c = Physics2D.OverlapCircleAll(transform.position, 0.75f, 1 << 8 | 1 << 9);

        if (c.Length > 1 && moving)
        {
            pos = transform.position;
        }

        // Movement
        transform.position = Vector3.MoveTowards(transform.position, pos, step);

        if (transform.position == pos) {
            //rotation
            Vector3 difference = pos + (new Vector3(0, -1)) - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

            moving = false;
        }
    }

    void ShootBullet()
    {
        int chance = Random.Range(0, 1);

        if (chance == 0)
        {
            Instantiate(bullet, new Vector3(transform.position.x + 0.3f, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 180));
            Instantiate(bullet, new Vector3(transform.position.x - 0.3f, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 180));
        }
    }

    void Movement()
    {
        pos = new Vector3(Random.Range(2.25f, -2.25f), Random.Range(3f, 3f));

        //rotation
        Vector3 difference = pos + (new Vector3(0, -1)) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

        moving = true;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name == "bullet(Clone)")
        {
            // Destroy the bullet
            Destroy(c.gameObject);

            health -= 1;

            //Change the Color
            currentColor = GetComponent<Renderer>().material.color;
            toColor = currentColor - new Color(0f, colorStep, colorStep, 0f);

            GetComponent<Renderer>().material.color = Color.Lerp(currentColor, toColor, 1);

            if (health <= 0 && !collided)
            {
                collided = true;

                bossDied();

                Destroy(gameObject);

            }
        }

       
    }

    void EndGame()
    {
        Destroy(gameObject);
    }
}
