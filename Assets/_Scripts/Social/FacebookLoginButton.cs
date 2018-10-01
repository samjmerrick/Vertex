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
    }

    public void ButtonPress()
    {
        if (!FB.IsLoggedIn)
        {
            FacebookPublicMethods.LogIn();
        }

        if(FB.IsLoggedIn)
        {
            Debug.Log("User already logged in"); //TODO - replace this with log out function
        }
    }
}
