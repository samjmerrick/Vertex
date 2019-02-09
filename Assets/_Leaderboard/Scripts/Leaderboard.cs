using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;
    public GameObject LoadingSymbol;
    public GameObject LeaderboardContent;

    public Text Score;
    public Text Rank;

    private GameObject _LoadingSymbol;

    private void OnEnable()
    {
        // Start a listener for changes to the scores table in Realtime database
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged += HandleValueChanged;
    }

    private void OnDisable()
    {
        // Stop listener
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged -= HandleValueChanged;

        DestroyChildren();
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        // Error check
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Instantiate the loading Symbol
        _LoadingSymbol = Instantiate(LoadingSymbol, LeaderboardContent.transform.parent);

        // Get a snapshot of the current data
        DataSnapshot Snapshot = args.Snapshot;

        // Clear existing UI entries
        DestroyChildren();

        // i is used as Rank
        int i = 1;

        // Reverse loop gives descending order
        foreach (var ChildSnapshot in Snapshot.Children.Reverse())
        {
            LeaderboardEntry entry = Instantiate(LeaderboardEntry, LeaderboardContent.transform).GetComponent<LeaderboardEntry>();

            entry.Init(
                rank: i,
                imageUrl: ChildSnapshot.Child("profilePicture").Value.ToString(),
                displayName: ChildSnapshot.Child("name").Value.ToString(),
                score: ChildSnapshot.Child("score").Value.ToString()
                );

            if (UserManager.GetUser() != null)
            {
                if (ChildSnapshot.Key.Equals(UserManager.GetUser().UserId))
                {
                    Score.text = "Your score is " + ChildSnapshot.Child("score").Value.ToString();
                    Rank.text = "Your rank is " + i;

                }
            }
         

            i++;
        }

        Destroy(_LoadingSymbol);
    }

    void DestroyChildren()
    {
        foreach (Transform child in LeaderboardContent.transform)
        {
            if (!child.name.Contains("_"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}

   


