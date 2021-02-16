using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

using System.Linq;

public class FirebaseDatabaseController : MonoBehaviour
{
    // Save info to database
    public static void SaveToDatabase(string location, Dictionary<string, object> data)
    {
        DatabaseReference db = FirebaseDatabase.DefaultInstance.RootReference;

        // Get location / UserID and set values
        db.Child(location).Child("ReplaceWithUserID").SetValueAsync(data);
        Debug.Log("Wrote data to " + location);
    }

    // Handle int entry at end of game
    public static void WriteNewHiScore(string name, int score)
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "name", name },
            { "score", score },
        };

        SaveToDatabase("scores", data);
    }
}
