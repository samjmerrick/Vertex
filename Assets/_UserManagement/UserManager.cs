using System.Collections;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;

public class UserManager : MonoBehaviour {

    private FirebaseAuth auth;

    private static FirebaseUser user;
    public static FirebaseUser GetUser() { return user; }

    public void Awake()
    {
        FacebookManager.Init();
        FirebaseAuthManager.Init();

        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public IEnumerator Start()
    {
        yield return new WaitUntil(() => FB.IsInitialized && FirebaseAuthManager.DependenciesMet);
        SignIn();
    }

    public void SignIn()
    {
        if (!FB.IsInitialized || !FirebaseAuthManager.DependenciesMet)
        {
            Debug.Log("Tried to sign in but some dependencies were not met");
            return;
        }
           

        if (!FB.IsLoggedIn)
        {
            Debug.Log("Tried to sign in but user is not signed in with Facebook");
            return;
        }

        FirebaseAuthManager.SignIntoFirebaseWithFacebook(AccessToken.CurrentAccessToken.TokenString);
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
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }
}
