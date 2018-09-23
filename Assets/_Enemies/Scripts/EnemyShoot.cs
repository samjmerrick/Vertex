using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform[] shootPoints;
    public float ShootSpeed;
    public float cooldown = 2;
    public int chanceToShoot;

    private bool isShrinking;
    private float targetShrink;
    private float shrinkSpeed = 0.8f;

    private void Start()
    {
        InvokeRepeating("Shoot", cooldown, cooldown);
        targetShrink = transform.localScale.x - .1f;
    }

    void Shoot()
    {
        if (Random.Range(0, 10) < chanceToShoot)
        {
            StartCoroutine(ShrinkAndShoot());
        }
    }

    IEnumerator ShrinkAndShoot()
    {
        isShrinking = true;
        yield return new WaitUntil(() => !isShrinking);

        foreach (Transform point in shootPoints)
        {
            GameObject go = Instantiate(Bullet, point.position, point.rotation);
            go.GetComponent<Rigidbody2D>().AddForce(-go.transform.up * ShootSpeed);
        }
    }

    private void Update()
    {
        if (isShrinking)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
     

            if (transform.localScale.x < targetShrink)
            {
                transform.localScale = new Vector3(targetShrink + .1f, targetShrink + .1f);
                isShrinking = false;
            
            }
        }
            
    }
}

