using UnityEngine;
using Firebase.Auth;

public static class FirebaseAuthManager {

    private static FirebaseUser user;
    public static FirebaseUser GetUser() { return user; }

    public static bool DependenciesMet;

    public static void Init()
    {
        // Code to check for Google play services - required for Android
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                DependenciesMet = true;
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

    public static void SignIntoFirebaseWithFacebook(string accessToken)
    {
        FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Credential credential =
            FacebookAuthProvider.GetCredential(accessToken);


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

            SaveManager.LoadFromDatabase(); // TODO - create event to trigger this

            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });
    }

    public static void SignOut()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignOut();

    }
}
