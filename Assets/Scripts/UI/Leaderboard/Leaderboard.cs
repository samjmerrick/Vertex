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

