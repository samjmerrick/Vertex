using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitOnDestroy : MonoBehaviour {

    public GameObject objectToSpawn;
    public int numberToSpawn;

    private Vector2 bounds;
    private bool isQuitting = false;

    private void OnDestroy()
    {
        // Check there is an object to spawn, and the game is running
        if (objectToSpawn != null && GameController.GameRunning && !isQuitting)
        {
            // Check we're above the bottom of the screen + 1
            bounds = Bounds.Get();
            if (transform.position.y > -bounds.y + 1)
            {
                for (int i = 0; i < numberToSpawn; i++)
                {
                    Instantiate(
                        original: objectToSpawn,
                        position: transform.position,
                        rotation: Quaternion.Euler(0, 0, Random.Range(0, 360))
                        );
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
