using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class SaveLeaderboardScore : MonoBehaviour
{


    // Handle int entry at end of game
    public void WriteNewHiScore(string name)
    {
        int score = Stats.gameStats["Destroyed"];

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "name", name },
            { "score", score },
        };

        SaveToDatabase("scores", data);
    }

    public void SaveToDatabase(string location, Dictionary<string, object> data)
    {
        DatabaseReference db = FirebaseDatabase.DefaultInstance.RootReference;

        // Get location / UserID and set values
        db.Child(location).Child("Replace-with-ID").SetValueAsync(data);
        Debug.Log("Wrote data to " + location);
    }
}
