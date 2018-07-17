using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveOnPath : MonoBehaviour {

    public float Speed = 4;
    public float RotSpeed = 200;
    public MovementPath[] PathOptions;
    public float MaxDistanceToGoal = .1f;

    [HideInInspector] public MovementPath MyPath;
    [HideInInspector] public GameObject leader; // Gets set in EnemyGroup

    private Rigidbody2D rb;
    private Transform target;
    private int movingTo = 0;
    private int movementDirection = 1;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (leader == null)
        {
            MyPath = PathOptions[Random.Range(0, PathOptions.Length)]; // Choose a random path
        }

        else
        {
            MyPath = leader.GetComponent<EnemyMoveOnPath>().MyPath;
        }

        transform.position = MyPath.EntryPoint.position; // Jump to entry point
        target = MyPath.PathSequence[0]; // Set first position

        // Snap Rotation towards target
        Vector3 difference = target.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void Update()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * RotSpeed;

        rb.velocity = transform.up * Speed;

        //Check to see if you are close enough to the next point to start moving to the following one
        var distanceSquared = (transform.position - target.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            GetNextPoint();
        }
    }

    void GetNextPoint()
    {
        movingTo += movementDirection;

        if (MyPath.PathType == MovementPath.PathTypes.loop)
        {
            if (MyPath.PathSequence.Length <= movingTo)
            {
                movingTo = 0;
            }
        }

        else if (MyPath.PathType == MovementPath.PathTypes.linear)
        {
            //If you are at the begining of the path
            if (movingTo <= 0)
            {
                movementDirection = 1;
            }
            //Else if you are at the end of your path
            else if (movingTo >= MyPath.PathSequence.Length - 1)
            {
                movementDirection = -1;
            }
        }

        target = MyPath.PathSequence[movingTo];
    }
}
