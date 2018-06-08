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
            if (FirebaseUser.user != null)
                buttonText.text = "Signed in as " + FirebaseUser.user.DisplayName;

            else
                buttonText.text = "FB/FBASE Mismatch";
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

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
