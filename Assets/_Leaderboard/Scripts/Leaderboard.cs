using UnityEngine;
using Firebase.Database;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;
    public GameObject LoadingSymbol;

    private void Start()
    { 
        FirebaseDatabase.DefaultInstance
        .GetReference("scores")
        .OrderByChild("score")
        .ValueChanged += HandleValueChanged;

    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        GameObject loadingSymbol = Instantiate(LoadingSymbol, transform);

        DataSnapshot snapshot = args.Snapshot;

        DestroyChildren();

        int i = 1;

        foreach (var ChildSnapshot in snapshot.Children.Reverse())
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

        Destroy(loadingSymbol);
    }

    void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            if (!child.name.Contains("_"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}

   


