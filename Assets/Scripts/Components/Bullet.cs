using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject DestroyEffect;
    public int MoveSpeed;
    public string CollisionTag;

    void Start()
    {
        // Push the object in the direction it is facing
        GetComponent<Rigidbody2D>().AddForce(transform.up * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == CollisionTag)
        {
            if (c.gameObject.tag == "Enemy")
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.DecreaseHealth();
                }
            }

            Destroy(gameObject);

            GameObject effect = Instantiate(DestroyEffect, transform.position, transform.rotation);
            Destroy(effect, 0.5f); 
        }
    }        

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}