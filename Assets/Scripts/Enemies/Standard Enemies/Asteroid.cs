using UnityEngine;
using System.Collections;

public class Asteroid : Enemy
{
    [HideInInspector] public bool isSplit = false;
    public GameObject[] SplittableObjects;

    void Start()
    {
        // Set to a random size
        float size = Random.Range(-0.05f, 0.1f);
        transform.localScale += new Vector3(size, size);

        // Do not spawn on top of another item
        var radius = GetComponent<Renderer>().bounds.extents.magnitude;

        Collider2D[] c = Physics2D.OverlapCircleAll(transform.position, radius * 0.15f, 1 << 8);
        float directionX = Random.Range(0.2f, -0.2f);
        float directionY = Random.Range(0.2f, -0.2f);

        int ran = 0;

        while (c.Length > 1)
        { 
            transform.position += new Vector3(directionX ,directionY);
            c = Physics2D.OverlapCircleAll(transform.position, radius * 0.15f, 1 << 8);

            ran++;

            if (ran == 10)
            {
                Destroy(gameObject);
                break;  
            }
        }

        if (!isSplit)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + Random.Range(20.0f, -20.0f));
            // Push the asteroid in the direction it is facing
            GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-150.0f, -200f));
        }

        // Give a random angular velocity/rotation
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
            Destroy(gameObject);

        if (c.gameObject.tag == "PlayerFire")
            DecreaseHealth();

        if (Health == 0 && SplittableObjects.Length > 0)
        {
            float angle = 180 / SplittableObjects.Length;
            int ran = 0;

            foreach (GameObject ToSpawn in SplittableObjects)
            {
                GameObject go = Instantiate(ToSpawn, transform.position, transform.rotation);
                go.GetComponent<Asteroid>().isSplit = true;

                go.transform.rotation = Quaternion.Euler(0, 0, go.transform.rotation.z + (angle * ran));

                go.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + ((Vector2)go.transform.up); 
                
                ran++;
            }
        }
    }
}

