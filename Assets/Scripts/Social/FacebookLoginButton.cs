using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookLoginButton : MonoBehaviour {

    public FbController fbController;
    public Text buttonText;

    private void OnEnable()
    {
        if (FB.IsLoggedIn)
        {
            AccessToken aToken = AccessToken.CurrentAccessToken;
            
            if (FirebaseUser.user != null)
                buttonText.text = "Signed in as " + FirebaseUser.user.DisplayName;

            else
                buttonText.text = "Signed in as Error 1";
        }
    }

    public void DoButtonAction()
    {
        if (FB.IsLoggedIn)
        {
            fbController.LogOut();
        }

        else
        {
            fbController.LogIn();
        }
    }
}
