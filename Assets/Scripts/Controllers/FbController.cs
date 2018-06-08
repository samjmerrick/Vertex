using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FirebaseUser))]
public class FbController : MonoBehaviour {

	List<string> perms = new List<string>(){"public_profile", "email"};

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

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();

            // Sign into Firebase
            if (FB.IsLoggedIn)
                GetComponent<FirebaseUser>().SignInWithFacebook(AccessToken.CurrentAccessToken.TokenString);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void AuthCallback (ILoginResult result)
    {
		if (FB.IsLoggedIn)
        {
			// AccessToken class will have session details
			var aToken = AccessToken.CurrentAccessToken;
            
            // Print current access token's User ID
            Debug.Log(aToken.UserId);

			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

            // Restart the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
			Debug.Log("User cancelled login");
		}
	}

    public void FeedShare()
    {
        if (FB.IsLoggedIn)
        {
            FB.FeedShare(
              link: new System.Uri("https://www.spaceygame.co.uk/"),
              linkName: "Spacey Game",
              linkCaption: "I destroyed " + Stats.gameStats["Destroyed"] + " aliens! Can you beat it?",
              linkDescription: "Spacey Game is a free mobile game",
              mediaSource: Application.persistentDataPath + "record.gif"
            );

            Debug.Log(Application.persistentDataPath + "record.gif");
        }
        else {
            Debug.Log("Not logged in");
        }
        
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void LogIn()
    {
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    public void LogOut()
    {
        FB.LogOut();
    }
}
