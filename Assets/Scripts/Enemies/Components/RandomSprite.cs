using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {
 
    public Sprite[] Options;

    private void Start()
    {
        int sprite = Random.Range(0, Options.Length);
        GetComponent<SpriteRenderer>().sprite = Options[sprite];
    }
}
