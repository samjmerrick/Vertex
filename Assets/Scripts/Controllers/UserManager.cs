using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour {

    public static Firebase.Auth.FirebaseUser user;
    private List<string> perms = new List<string>() { "public_profile", "email" };

    private bool allDependenciesMet = false;

    #region Init Methods

    public IEnumerator Start()
    {
        StartFacebookSDK();
        yield return new WaitUntil(() => FB.IsInitialized);

        CheckDependencies();
        yield return new WaitUntil(() => allDependenciesMet);

        if (FB.IsLoggedIn)
        {
            SignInWithFacebook(AccessToken.CurrentAccessToken.TokenString);
        }
        else
        {
            Debug.Log("User is not logged in with Facebook");
        }
	}
	

    private void CheckDependencies()
    {
        // Code to check for Google play services - required for Android
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                allDependenciesMet = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                Debug.Log(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    private void SignInWithFacebook(string accessToken)
    {
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
            user = auth.CurrentUser;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    #endregion

    #region Facebook Init Methods

    private void StartFacebookSDK()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
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
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    #endregion

    #region Public Methods

    public void LogIn()
    {
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;

            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            SignInWithFacebook(aToken.TokenString);

            // Restart the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void LogOut()
    {
        FB.LogOut();
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
        else
        {
            Debug.Log("Not logged in");
        }

    }

    #endregion
}
