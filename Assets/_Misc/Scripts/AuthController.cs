using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    public static string UID;

    public static void AnonymousLogin()
    {
        FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            
            UID = newUser.UserId;

            AnalyticsController.LoggedIn();
        });
    }
}
