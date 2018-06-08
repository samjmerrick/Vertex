using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;


    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://star-defender.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance
            .GetReference("scores")
            .OrderByChild("score")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    int i = 1;

                    foreach(var ChildSnapshot in snapshot.Children.Reverse())
                    {
                        LeaderboardEntry entry = Instantiate(LeaderboardEntry, transform).GetComponent<LeaderboardEntry>();

                        entry.Init(
                            rank: i,
                            imageUrl: ChildSnapshot.Child("profilePicture").Value.ToString(),
                            displayName: ChildSnapshot.Child("name").Value.ToString(),
                            score: ChildSnapshot.Child("score").Value.ToString()
                            );

                        i++;
                    }
                }
            });
    }
}

