using UnityEngine;

public class EnableParticlesOnGameStart : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameBegin += Enable;
        GameController.GameEnd += Disable;
    }

    private void OnApplicationQuit()
    {
        GameController.GameBegin -= Enable;
        GameController.GameEnd -= Disable;
    }

    private void Start()
    {
        Disable();
    }

    private void Disable()
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
