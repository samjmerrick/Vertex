using UnityEngine;
using System.Collections;

public class EuclideanTorus : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Teleport the game object
        if (transform.position.x > 3)
        {
            transform.position = new Vector3(-3, transform.position.y, 0);
        }
        else if (transform.position.x < -3)
        {
            transform.position = new Vector3(3, transform.position.y, 0);
        }

        else if (transform.position.y > 5)
        {
            Destroy(gameObject);
        }

        else if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}