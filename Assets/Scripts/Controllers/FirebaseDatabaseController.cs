using System.Collections;
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
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://star-defender.firebaseio.com/");

        // Get the root reference location of the database.
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static Dictionary<string, object> GetBestStats()
    {
        if (UserManager.user == null)
        {
            Debug.Log("User not signed in");
            return new Dictionary<string, object>();
        }

        Dictionary<string, object> bestStats = new Dictionary<string, object>();

        FirebaseDatabase.DefaultInstance
           .GetReference("best-stats")
           .Child(UserManager.user.UserId)
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
                       bestStats.Add(ChildSnapshot.Key, ChildSnapshot.Value);
                   }

                   Debug.Log("Retrieved stats: " + bestStats);
				
					foreach (KeyValuePair<string, object> entry in Stats.bestStats){
						Debug.Log (entry.Key + ": " + entry.Value);
					}
               }
           });

        return bestStats;
    }

    public static void SaveToDatabase(string location, Dictionary<string, object> data)
    {
        if (UserManager.user != null)
        {
            db.Child(location).Child(UserManager.user.UserId).SetValueAsync(data);
            Debug.Log("Wrote data to " + location);
        }

        else
        {
            Debug.Log("Data not written in *" + location + "* as user not signed in");
        }
    }

	public static void WriteNewHiScore(object score)
    {
        Firebase.Auth.FirebaseUser user = UserManager.user;

        if (user == null) return;
	

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
