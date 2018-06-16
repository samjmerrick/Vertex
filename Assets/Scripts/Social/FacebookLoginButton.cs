using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookLoginButton : MonoBehaviour {

    public Text buttonText;

    private void OnEnable()
    {
        if (FB.IsLoggedIn)
        {
            if (UserManager.GetUser() != null)
            {
				buttonText.text = "Signed in as " + UserManager.GetUser().DisplayName;
				GetComponent<Button> ().enabled = false;
			}

            else
            {
                buttonText.text = "Loading info";
            }        
        }
    }
}
