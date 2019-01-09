using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitOnDestroy : MonoBehaviour {

    public GameObject objectToSpawn;
    public int numberToSpawn;

    private void OnTriggerEnter2D(Collider2D c)
    {
        // Check collision is with PlayerFire and our health is 0
        if (c.gameObject.tag == "PlayerFire" && GetComponent<Enemy>().Health == 0)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                Instantiate(
                    original: objectToSpawn,
                    position: transform.position,
                    rotation: Quaternion.Euler(0, 0, Random.Range(-90, 90))
                    );
            } 
        }
    }
}
