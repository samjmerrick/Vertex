using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FbController : MonoBehaviour {
    
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

            string accessToken = AccessToken.CurrentAccessToken.ToString();

            Firebase.Auth.Credential credential =
                Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);


            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

            auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });



            // Print current access token's User ID
            Debug.Log(aToken.UserId);

			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

            // Restart the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else {
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
              linkCaption: "I destroyed " + GameController.gameStats["Destroyed"] + " aliens! Can you beat it?",
              linkDescription: "Spacey Game is a free mobile game",
              mediaSource: Application.persistentDataPath + "record.gif"
            );

            Debug.Log(Application.persistentDataPath + "record.gif");
        }
        else {
            Debug.Log("Not logged in");
        }
        
    }
	
}
