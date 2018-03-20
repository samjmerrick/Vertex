using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvMoveOLD : MonoBehaviour
{
    public GameObject bullet;

    private Vector3 pos;
    private float speed = 0;
    private Color currentColor;
    private Color toColor;
    private bool collided = false;
    private bool complete = false;
    private int health = 30;

    private float lastTouched;

    public delegate void EnemyDied(Vector3 transform, string name);
    public static event EnemyDied enemyDied;

    // Use this for initialization
    void Start()
    {

        pos = transform.position;

        var child = transform.GetSiblingIndex();

        //Randomise location
        var xAxis = Random.Range(0, 2) == 0 ? -5 : 5;
        transform.position = transform.position + new Vector3(xAxis, Random.Range(5, -5));

        InvokeRepeating("ShootBullet", 1, 2);
        InvokeRepeating("Movement", 4, 1);
        Invoke("StartMove", child * 0.2f);
    }

    private void StartMove()
    {
        speed = 4;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        //rotation
        Vector3 difference = pos + (new Vector3(0, -1)) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

        if (!complete)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, step);
        }

        if (transform.position == pos)
        {
            complete = true;
            pos += new Vector3(0, -10);
        }

        
    }

    void ShootBullet()
    {
        int chance = Random.Range(0, 4);

        if (chance == 0)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 180));
        }
    }

    void Movement()
    {

        transform.position += new Vector3(0, -0.1f);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Bullet") || c.gameObject.tag.Equals("Shield"))
        {
            if (c.gameObject.name == "bullet(Clone)")
            {
                // Destroy the bullet
                Destroy(c.gameObject);
            }

            health -= 10;

            //Change the Color
            currentColor = GetComponent<Renderer>().material.color;
            toColor = currentColor - new Color(0f, 0.3f, 0.3f, 0f);

            GetComponent<Renderer>().material.color = Color.Lerp(currentColor, toColor, 1);

            if (health <= 0 && !collided)
            {
                collided = true;

                enemyDied(transform.position, name);

                Destroy(gameObject);
            }
        }
    }
}
