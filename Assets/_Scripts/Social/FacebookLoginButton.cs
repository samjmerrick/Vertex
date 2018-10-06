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
            if (UserManager.GetUser() != null)
            {
				buttonText.text = "Signed in as " + UserManager.GetUser().DisplayName;
			}

            else
            {
                buttonText.text = "Loading info";
            }        
        }

        else
        {
            buttonText.text = "Connect with Facebook";
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

            Debug.Log("Logged out"); //TODO - replace this with log out function
        }
    }
}
