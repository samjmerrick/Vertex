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

    private float shrinkAmount = .2f;
    private float shrinkSpeed = 0.6f;

    private void Start()
    {
        InvokeRepeating("CheckIfShooting", cooldown, cooldown);
    }

    void CheckIfShooting()
    {
        if (Random.Range(0, 10) < chanceToShoot)
        {
            StartCoroutine(ShrinkAndShoot());
        }
    }

    IEnumerator ShrinkAndShoot()
    {
        float shrinkTarget = transform.localScale.x - shrinkAmount;

        // Shrink
        while (transform.localScale.x > shrinkTarget)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            yield return new WaitForFixedUpdate();
        }

        // Back to full size
        transform.localScale = new Vector3(shrinkTarget + shrinkAmount, shrinkTarget + shrinkAmount);
        Shoot();
    }

    void Shoot()
    {
        foreach (Transform point in shootPoints)
        {
            GameObject go = Instantiate(Bullet, point.position, point.rotation);
            go.GetComponent<Rigidbody2D>().AddForce(-go.transform.up * ShootSpeed);
        }
    }
}

