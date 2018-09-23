﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform[] shootPoints;
    public float ShootSpeed;
    public float cooldown = 2;
    public int chanceToShoot;
    
    private void Start()
    {
        InvokeRepeating("Shoot", cooldown, cooldown);
    }

    void Shoot()
    {
        if (Random.Range(0, 10) < chanceToShoot)
        {
            foreach (Transform point in shootPoints)
            {
                GameObject go = Instantiate(Bullet, point.position, point.rotation);
                go.GetComponent<Rigidbody2D>().AddForce(-go.transform.up * ShootSpeed);
            }
        }
    }
}

