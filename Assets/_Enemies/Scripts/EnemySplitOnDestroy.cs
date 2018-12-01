using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitOnDestroy : MonoBehaviour {

    public GameObject[] thingsToSpawn;

    private void OnDestroy()
    {
        if (thingsToSpawn.Length > 0 && GameController.GameRunning)
        {
            float angle = 180 / thingsToSpawn.Length;
            int ran = 0;

            foreach (GameObject toSpawn in thingsToSpawn)
            {
                Instantiate(
                    original: toSpawn,
                    position: transform.position,
                    rotation: Quaternion.Euler(0, 0, Random.Range(0, 360))
                    );

                ran++;
            }
        }
    }
}
