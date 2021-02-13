using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookLoginButton : MonoBehaviour {

    public string NotSignedInMessage;
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
            buttonText.text = NotSignedInMessage;
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
            transform.parent.gameObject.SetActive(false);
            WarningMessage.SetActive(true);  
        }
    }

    public void SignOut()
    {
        FacebookManager.LogOut();

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }
}
