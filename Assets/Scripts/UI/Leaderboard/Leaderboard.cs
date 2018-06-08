using UnityEngine;
using Firebase.Database;
using System.Linq;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;

    public Text info;

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

        string user = "null";
        if (FirebaseUser.user != null) user = FirebaseUser.user.DisplayName;

        if (info != null)
            info.text = "Your hi-score is: " + Stats.bestStats["Destroyed"] + ".    Signed in as: " + user;
    }
}

