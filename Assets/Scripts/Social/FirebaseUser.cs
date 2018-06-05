using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseUser : MonoBehaviour {

    Firebase.Auth.FirebaseAuth auth;
    public static Firebase.Auth.FirebaseUser user;

    public static string UserID;
    public static string displayName;
    public static string emailAddress;
    public static System.Uri photoUrl;

    void awake()
    {
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? null;
                UserID = user.UserId;
            }
        }
    }

    public void SignInWithFacebook(string accessToken)
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
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
