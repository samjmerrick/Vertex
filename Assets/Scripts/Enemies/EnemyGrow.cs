using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrow : MonoBehaviour {

    public float Speed;
    public Vector2 MinMax;

	void Update ()
    {
        var range = MinMax.y - MinMax.x;

        float scale = (Mathf.Sin(Time.time * Speed) + 1.0f) / 2.0f * range + MinMax.x;
        transform.localScale = new Vector3(scale, scale);
    }
}
