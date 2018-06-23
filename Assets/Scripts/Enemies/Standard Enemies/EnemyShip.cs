using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Enemy {

    public MovementPath MyPath;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> pointInPath; //Used to reference points returned from MyPath.GetNextPathPoint
    private Rigidbody2D rb;

    public void Start()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }

        //Sets up a reference to an instance of the coroutine GetNextPathPoint
        pointInPath = MyPath.GetNextPathPoint();

        //Get the next point in the path to move to (Gets the Default 1st value)
        pointInPath.MoveNext();

        //Make sure there is a point to move to
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return; //Exit Start() if there is no point to move to
        }

        //Set the position of this object to the position of our starting point
        transform.position = pointInPath.Current.position;

        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //Validate there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null) return; //Exit if no path is found

        Vector2 direction = (Vector2)pointInPath.Current.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * RotSpeed;

        rb.velocity = transform.up * Speed;


        //Check to see if you are close enough to the next point to start moving to the following one
        //Using Pythagorean Theorem
        //per unity suaring a number is faster than the square root of a number
        //Using .sqrMagnitude 
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        //The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)
        /*
        var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
        if (distanceSquared < MaxDistanceToGoal) //If you are close enough
        {
            pointInPath.MoveNext(); //Get next point in MovementPath
        }
        */
    }

    private void OnBecameInvisible()
    {
        // Override Enemy class (Do nothing).
    }
}


