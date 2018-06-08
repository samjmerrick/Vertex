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
        if (FirebaseUser.user == null)
        {
            Debug.Log("User not signed in");
            return new Dictionary<string, object>();
        }

        Dictionary<string, object> bestStats = new Dictionary<string, object>();

        FirebaseDatabase.DefaultInstance
           .GetReference("stats")
           .Child(FirebaseUser.user.UserId)
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
               }
           });

        return bestStats;
    }

    public static void SaveBestStats(Dictionary<string, object> bestStats)
    {
        if (FirebaseUser.user != null)
        {
            db.Child("best-stats").Child(FirebaseUser.user.UserId).SetValueAsync(bestStats);
            Debug.Log("Wrote stats: " + bestStats);
        }

        else
        {
            Debug.Log("Best stats not written as user not signed in");
        }
    }
}
