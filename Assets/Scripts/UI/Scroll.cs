using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    public float speed;
    private float calcSpeed;
   
    void OnEnable()
    {
        GameController.GameBegin += Enable;
        GameController.GameEnd += Disable;
    }

    void OnDisable()
    {
        GameController.GameBegin += Enable;
        GameController.GameEnd += Disable;
    }
    private void Start()
    {
        calcSpeed = speed * 0.5f;
    }

    private void Disable()
    {
        calcSpeed = speed * 0.5f;
    }
    private void Enable()
    {
        calcSpeed = speed * 1.5f;
    }


    // Update is called once per frame
    public void Update () {

        float y = GetComponent<Renderer>().material.mainTextureOffset.y + (calcSpeed * Time.deltaTime);

        Vector2 offset = new Vector2(0, y);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
