using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public GameObject DestroyEffect;
    public int MoveSpeed;
    private Vector3 firePos;

    void Start()
    {
        firePos = transform.position;
        // Push the object in the direction it is facing
        GetComponent<Rigidbody2D>().AddForce(transform.up * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            // Only calculate difference for enemies without 
            if (!c.gameObject.name.Contains("_"))
            {
                float diff = Vector2.Distance(Camera.main.WorldToViewportPoint(firePos), Camera.main.WorldToViewportPoint(transform.position)) * 100;

                if (diff < 10)
                    UIControl.instance.GameMessage("Close call! (" + (int)diff + "m)");

                if (diff > 60)
                    UIControl.instance.GameMessage("Long shot! (" + (int)diff + "m)");
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