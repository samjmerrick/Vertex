using UnityEngine;
using System.Collections.Generic;
using Facebook.Unity;

public static class FacebookManager {

    private static List<string> perms = new List<string>() { "public_profile", "email" };

    public static void Init()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private static void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    public static void LogIn()
    {
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    public static void LogOut()
    {
        if(FB.IsLoggedIn)
            FB.LogOut();
    }

    private static void AuthCallback(ILoginResult result)
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

            // Initialise UserManager, this will sign the facebook user into firebase auth
            GameObject.FindObjectOfType<UserManager>().Init();
        }

        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public static void FeedShare()
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
