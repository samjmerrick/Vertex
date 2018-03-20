using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInvisible : MonoBehaviour
{

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}