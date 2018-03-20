using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersMovement : MonoBehaviour {

    private float speed = 3f;

    private Vector3 pos;
    private bool movdir;
    private float lastMoved;
    private int xmovcount;
    private int spawnType;

    public static int enemiesRemaining;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pos, (speed + 8) * Time.deltaTime);

        if (Time.time - lastMoved > 1 / speed)
        {
            if (xmovcount != 3)
            {
                float movementX = movdir ? 0.4f : -0.4f;
                pos += new Vector3(movementX, 0, 0);
                xmovcount++;
            }

            else
            {
                movdir = !movdir;
                pos -= new Vector3(0, 0.75f); //drop
                xmovcount = 0;
            }

            lastMoved = Time.time;
            speed += (GameController.score * 0.00005f);
        }
    }
}
