using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookProfileImage : MonoBehaviour {

    private void OnEnable()
    {
        if (FB.IsLoggedIn)
        {
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
        }
    }

    private void GetPicture(IGraphResult result)
    {
        if (result.Error == null && result.Texture != null)
        {
            GetComponentInChildren<RawImage>().texture = result.Texture;
            GetComponent<Button>().enabled = false;
        }
    }

    private void OnDisable()
    {
        
    }
}
