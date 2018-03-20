using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float Health = 3;
    public string CollisionTag;

    private float colorStep;
    private Color currentColor;
    private Color toColor;

    // Use this for initialization
    void Start()
    {
        colorStep = (1 / Health);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == CollisionTag) {

            Health -= 1;

            if (Health == 0)
            {
                Destroy(gameObject);
            }

            //Change the Color
            currentColor = GetComponent<Renderer>().material.color;
            toColor = currentColor - new Color(0f, 0f, 0f, colorStep);

            GetComponent<Renderer>().material.color = Color.Lerp(currentColor, toColor, 1);
        }    
    }
}
