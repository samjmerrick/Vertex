using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour {

    private static Firebase.Auth.FirebaseUser user;
    public static Firebase.Auth.FirebaseUser GetUser() { return user; }

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
            SignIntoFirebaseWithFacebook(AccessToken.CurrentAccessToken.TokenString);
        }
        else
        {
            Debug.Log("Can't sign into firebase as user is not logged in with Facebook");
        }
    }

    private void SignIntoFirebaseWithFacebook(string accessToken)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Firebase.Auth.Credential credential =
                Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);


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

            user = task.Result;

            SaveManager.LoadFromDatabase();

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });
    }

    #endregion

    #region Init Methods

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

    #endregion


}
