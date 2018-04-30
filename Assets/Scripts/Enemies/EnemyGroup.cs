using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject Enemy;
    public int GroupSize;
    public float spacing;

    private void Start()
    {
        for (int i = 0; i < GroupSize; i++)
        {
            Instantiate(Enemy,
                        transform.position + new Vector3(0, (i + 1) * spacing),
                        transform.rotation,
                        transform);
        }
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

}
