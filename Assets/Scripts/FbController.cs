using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FbController : MonoBehaviour {

	public GameObject FacebookIcon;
	List<string> perms = new List<string>(){"public_profile", "email"};

	public void LogIn(){
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	// Awake function from Unity's MonoBehavior
	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}

		if (FB.IsLoggedIn){
			FB.API( "/me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void AuthCallback (ILoginResult result)
    {
		if (FB.IsLoggedIn)
        {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			// Print current access token's User ID
			Debug.Log(aToken.UserId);

			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

            FacebookIcon.GetComponent<Button>().enabled = false;
		}

        else {
			Debug.Log("User cancelled login");
		}
	}
	private void GetPicture(IGraphResult result)
    {
		if (result.Error == null && result.Texture != null)
        {
			FacebookIcon.GetComponentInChildren<RawImage>().texture = result.Texture;
		}
	}
}
