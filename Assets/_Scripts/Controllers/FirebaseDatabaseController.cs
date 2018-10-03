using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseDatabaseController : MonoBehaviour {

    public static DatabaseReference db;

    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vertex-game.firebaseio.com/");

        // Get the root reference location of the database.
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static Dictionary<string, int> GetFromDatabase(string location)
    {
        Firebase.Auth.FirebaseUser user = FirebaseAuthManager.GetUser();

        Dictionary<string, int> bestStats = new Dictionary<string, int>();

        FirebaseDatabase.DefaultInstance
           .GetReference("best-stats")
           .Child(user.UserId)
           .GetValueAsync().ContinueWith(task => {
               if (task.IsFaulted)
               {
                    // Handle the error...
               }
               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   foreach (var ChildSnapshot in snapshot.Children)
                   {
                       bestStats.Add(ChildSnapshot.Key, Convert.ToInt32(ChildSnapshot.Value));
                   }

                   Debug.Log("Retrieved stats: " + bestStats);
				
					foreach (KeyValuePair<string, int> entry in Stats.bestStats){
						Debug.Log (entry.Key + ": " + entry.Value);
					}
               }
           });

        return bestStats;
    }

    // Save info to database
    public static void SaveToDatabase(string location, Dictionary<string, object> data)
    {
        Firebase.Auth.FirebaseUser user = FirebaseAuthManager.GetUser();

        // Get location / UserID and set values
        db.Child(location).Child(user.UserId).SetValueAsync(data);
        Debug.Log("Wrote data to " + location);
    }

    // Extension class to handle <string, int> conversion to <string, object>
    public static void SaveToDatabase(string location, Dictionary<string, int> data)
    {
        SaveToDatabase(location, data.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value));
    }

    // Handle int entry at end of game
    public static void WriteNewHiScore(int score)
    {
        Firebase.Auth.FirebaseUser user = FirebaseAuthManager.GetUser();

        string profileImage = "http://graph.facebook.com/" + Facebook.Unity.AccessToken.CurrentAccessToken.UserId + "/picture";

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "userID", user.UserId.ToString() },
			{ "profilePicture", profileImage },
            { "score", score },
            { "name", user.DisplayName }
        };

        SaveToDatabase("scores", data);
    }
}
