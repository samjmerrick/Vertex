using UnityEngine;

public class EnableParticlesOnGameStart : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameBegin += Enable;
        GameController.GameEnd += Disable;
    }

    private void OnDisable()
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
        GetComponent<ParticleSystem>().Stop();
    }

    void Enable()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
