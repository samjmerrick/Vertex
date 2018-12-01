using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAlternating : EnemyShoot
{
    private void Start()
    {
        InvokeRepeating("Shoot", cooldown, cooldown);
    }

    IEnumerator Shoot()
    {
        if (Random.Range(0, 10) < chanceToShoot)
        {
            foreach (Transform point in shootPoints)
            {
                GameObject go = Instantiate(Bullet, point.position, point.rotation);
                go.GetComponent<Rigidbody2D>().AddForce(-go.transform.up * ShootSpeed);

                // Waits until next shot
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}