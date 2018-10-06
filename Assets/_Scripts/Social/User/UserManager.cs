using System.Collections;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;

public class UserManager : MonoBehaviour {

    private FirebaseAuth auth;

    private static FirebaseUser user;
    public static FirebaseUser GetUser() { return user; }

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        StartCoroutine(Start());
    }

    public IEnumerator Start()
    {
        FacebookManager.Init();
        yield return new WaitUntil(() => FB.IsInitialized);

        FirebaseAuthManager.Init();
        yield return new WaitUntil(() => FirebaseAuthManager.DependenciesMet);

        if (FB.IsLoggedIn)
        {
            FirebaseAuthManager.SignIntoFirebaseWithFacebook(AccessToken.CurrentAccessToken.TokenString);
        }

        else
        {
            Debug.Log("Can't sign into firebase as user is not logged in with Facebook");
        }
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
            user = auth.CurrentUser;s

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
