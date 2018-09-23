using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.IO;

public class FacebookProfileImage : MonoBehaviour
{
    private void OnEnable()
    {
        if (SaveManager.FileExists("ProfileImage.png") && FB.IsLoggedIn)
        {
            GetComponentInChildren<RawImage>().texture = SaveManager.LoadTextureToFile("ProfileImage.png");
        }

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

            SaveManager.SaveTextureToFile(result.Texture, "ProfileImage.png");
        }
    }
}
