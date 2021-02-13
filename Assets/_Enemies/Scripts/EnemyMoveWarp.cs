using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveWarp : MonoBehaviour {

    public float Speed = 20;
    public float maxDistance = 1;
    public float WaitTime = 1;

    private Vector2 target;
    private Vector2 bounds;

	void Start ()
    {
        bounds = Bounds.Get();
        StartCoroutine(ChooseNextPoint());
	}

	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
	}

    IEnumerator ChooseNextPoint()
    {
        while (enabled)
        {
            Vector2 pos = transform.position;
            target.x = Random.Range(pos.x - maxDistance, pos.x + maxDistance);
            target.y = Random.Range(pos.y - maxDistance, pos.y);

            if (target.x >= bounds.x + 2f || target.x <= bounds.x - 2f)
                target.x = -target.x;

            yield return new WaitForSeconds(WaitTime);
        }
    }
}
