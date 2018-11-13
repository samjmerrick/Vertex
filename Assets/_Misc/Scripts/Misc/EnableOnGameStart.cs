using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnGameStart : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameBegin += Enable;
    }

    private void OnDisable()
    {
        GameController.GameBegin -= Enable;
    }

    private void Start()
    {
        var emission = GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
    }

    void Enable()
    {
        var emission = GetComponent<ParticleSystem>().emission;
        emission.enabled = true;
    }
}
