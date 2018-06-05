using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseUser : MonoBehaviour {

    public static Firebase.Auth.FirebaseUser user;

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
            user = auth.CurrentUser;

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
