using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookLoginButton : MonoBehaviour {

    public GameObject WarningMessage;
    public Text buttonText;

    private void OnGUI()
    {
        if (FB.IsLoggedIn)
        {
            if (FirebaseAuthManager.GetUser() != null)
            {
				buttonText.text = "Signed in as " + FirebaseAuthManager.GetUser().DisplayName;
			}

            else
            {
                buttonText.text = "Loading info";
            }        
        }
    }

    public void ButtonPress()
    {
        if (!FB.IsLoggedIn)
        {
            FacebookManager.LogIn();
        }

        if(FB.IsLoggedIn)
        {
            FacebookManager.LogOut();

            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.SignOut();

            Debug.Log("User already logged in"); //TODO - replace this with log out function
        }
    }
}
