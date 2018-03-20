using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

	    foreach(Transform child in transform)
        {
            child.GetComponent<Rigidbody2D>().AddForce(transform.up * -100);
        }
	}

}
