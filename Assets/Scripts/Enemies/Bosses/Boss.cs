using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public int Speed;

    public BossAttack[] Attacks;

    public int reloadTime;

    private Vector2 target;
    private GameObject ship;

    private void Start()
    {
        StartCoroutine(Shoot());
        ship = GameObject.FindGameObjectWithTag("Player");
        target.y = 3;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
    }

    public bool IsMoving()
    {
        return target != (Vector2)transform.position;
 
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (!IsMoving())
            {
                int a = Random.Range(0, Attacks.Length);

                StartCoroutine(Attacks[a].Attack());

                yield return new WaitUntil(Attacks[a].IsEmpty);

                Evade();
                yield return new WaitForSeconds(reloadTime);
            }
            else
            {
                // Check again in .2 seconds
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    void Evade()
    {
        target.x = ship.transform.position.x;

    }

    private void OnBecameInvisible()
    {
        // Override Enemy class (Do nothing).
    }
}
