using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashInGame : MonoBehaviour {

    Color32 _colorRef;
    Image FlashImage;

    private void OnEnable()
    {
        Pickup.Got += Flash; 
    }

    private void OnDisable()
    {
        Pickup.Got -= Flash;
    }

    private void Start()
    {
        FlashImage = GetComponent<Image>();
        _colorRef = FlashImage.color;
    }


    public void Flash(string name, int time)
    {
       StartCoroutine(DoFlash(new Color(0,1,0,0.5f)));
    }

    private IEnumerator DoFlash(Color color)
    {
        FlashImage.color = color;

        while (FlashImage.color != _colorRef)
        {
            FlashImage.color = Color.Lerp(FlashImage.color, _colorRef, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

    }

}
