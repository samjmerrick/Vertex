using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Firebase.Database;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;
    public GameObject MyLeaderboardEntry;
    public GameObject LoadingSymbol;
    public GameObject LeaderboardContent;

    private GameObject _LoadingSymbol;
    private RectTransform userScore;
    private bool firstRunAfterEnable = false; // This helps determine whether we should snap user to their score

    void OnEnable()
    {
        firstRunAfterEnable = true;

        // Reset to the top of the board when enabled
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged += HandleValueChanged;
    }

    void OnDisable()
    {
        if (_LoadingSymbol != null)
        {
            Destroy(_LoadingSymbol);
        }

        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged -= HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        DestroyChildren();

        // Instantiate the loading Symbol
        _LoadingSymbol = Instantiate(LoadingSymbol, LeaderboardContent.transform.parent);

        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        GenerateLeaderboard(args.Snapshot);
    }

    void GenerateLeaderboard(DataSnapshot snapshot)
    {
        // i is used as Rank
        int i = 1;

        // Reverse loop gives descending order
        foreach (var ChildSnapshot in snapshot.Children.Reverse())
        {
            LeaderboardEntry entry;
            string entryName = ChildSnapshot.Child("name").Value.ToString();

            // Instantiate editable entry if it is this user
            if (ChildSnapshot.Key == AuthController.UID)
            {
                entry = Instantiate(MyLeaderboardEntry, LeaderboardContent.transform).GetComponent<LeaderboardEntry>();
                userScore = entry.GetComponent<RectTransform>();
            }

            // Skip other entries without a name
            else if (entryName == "Your name" || entryName == "Anonymous" || entryName == "")
            {
                continue;
            }

            // Everyone else
            else
            {
                entry = Instantiate(LeaderboardEntry, LeaderboardContent.transform).GetComponent<LeaderboardEntry>();
            }

            entry.Init(
                id: ChildSnapshot.Key,
                rank: i,
                displayName: ChildSnapshot.Child("name").Value.ToString(),
                score: ChildSnapshot.Child("score").Value.ToString()
                );

            i++;
        }

        if (userScore != null && firstRunAfterEnable)
        {
            SnapTo(userScore);
        }

        firstRunAfterEnable = false;
        Destroy(_LoadingSymbol);
    }

    void DestroyChildren()
    {
        foreach (Transform child in LeaderboardContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SnapTo(RectTransform target)
    {
        RectTransform contentPanel = GetComponent<RectTransform>();
        ScrollRect scrollRect = GetComponent<ScrollRect>();

        Canvas.ForceUpdateCanvases();

        Vector2 anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        // Scroll down a little bit to bring score into full view
        anchoredPosition.y = anchoredPosition.y - 200;

        Debug.Log(anchoredPosition);

        contentPanel.anchoredPosition = anchoredPosition;
    }
}