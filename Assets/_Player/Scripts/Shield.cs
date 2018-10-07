using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    ShipTakeDamage sd;

    private void OnEnable()
    {
        sd = GetComponentInParent<ShipTakeDamage>();

        if (sd == null)
        {
            Debug.Log("Shield tried to disable ShipTakeDamage component, but none was found");
            enabled = false;
        }

        sd.enabled = false;
    }

    private void OnDisable()
    {
        sd.enabled = true;
    }
}
