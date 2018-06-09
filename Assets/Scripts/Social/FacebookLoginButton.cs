using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookLoginButton : MonoBehaviour {

    
    public Text buttonText;

    private void OnEnable()
    {
        if (FB.IsLoggedIn)
        {
            if (UserManager.user != null)
                buttonText.text = "Signed in as " + UserManager.user.DisplayName;

            else
                buttonText.text = "FB/FBASE Mismatch";
        }
    }

    public void DoButtonAction()
    {
        

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
