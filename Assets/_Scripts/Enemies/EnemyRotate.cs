using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public Vector2 AngularVelocity;

    // Use this for initialization
    void Start()
    {
        float angularVelocity = Random.Range(AngularVelocity.x, AngularVelocity.y);
        GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
    }

}
