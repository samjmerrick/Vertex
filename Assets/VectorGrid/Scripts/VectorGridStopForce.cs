using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorGridStopForce : MonoBehaviour {

    public float timeToStop;

	IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToStop);
        GetComponent<VectorGridForce>().enabled = false;
    }
}
