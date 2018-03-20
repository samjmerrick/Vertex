using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;
    public int frequency;
    public int shootChance;

    // Use this for initialization
    void Start () {
        InvokeRepeating("ShootBullet", frequency, frequency);
    }

    void ShootBullet()
    {
        int chance = Random.Range(0, shootChance);

        if (chance == 0)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, 180));
        }
    }
}
