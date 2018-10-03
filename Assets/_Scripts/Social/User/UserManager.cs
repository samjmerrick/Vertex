using System.Collections;
using UnityEngine;
using Facebook.Unity;

public class UserManager : MonoBehaviour {

    private bool allDependenciesMet = false;

    public void Init()
    {
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
}
