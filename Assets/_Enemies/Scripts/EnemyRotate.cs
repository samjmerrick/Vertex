using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public Vector2 AngularVelocity;

    // Use this for initialization
    void Start()
    {
        float angularVelocity = Random.Range(AngularVelocity.x, AngularVelocity.y);

        angularVelocity *= Random.Range(0, 2) * 2 - 1; // Random 1 or -1, 50% chance to be left or right rotation

        GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
    }
}
