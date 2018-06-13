using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FacebookPublicMethods : MonoBehaviour {

    private List<string> perms = new List<string>() { "public_profile", "email" };

    public void LogIn()
    {
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;

            // Print current access token's User ID
            Debug.Log(aToken.UserId);

            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            // Restart the scene - Usermanager will pick up newly signed in user and attempt firebase sign-in
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void LogOut()
    {
        FB.LogOut();
    }

    public void FeedShare()
    {
        if (FB.IsLoggedIn)
        {
            FB.FeedShare(
              link: new System.Uri("https://www.spaceygame.co.uk/"),
              linkName: "Spacey Game",
              linkCaption: "I destroyed " + Stats.gameStats["Destroyed"] + " aliens! Can you beat it?",
              linkDescription: "Spacey Game is a free mobile game",
              mediaSource: Application.persistentDataPath + "record.gif"
            );

            Debug.Log(Application.persistentDataPath + "record.gif");
        }
        else
        {
            Debug.Log("Not logged in");
        }

    }
}
