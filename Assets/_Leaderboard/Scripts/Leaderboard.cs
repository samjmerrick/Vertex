using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Firebase.Database;

public class Leaderboard : MonoBehaviour
{
    public GameObject LeaderboardEntry;
    public GameObject LoadingSymbol;
    public GameObject LeaderboardContent;

    private GameObject _LoadingSymbol;
    private RectTransform userScore;

    private void OnEnable()
    {
        // Start a listener for changes to the scores table in Realtime database
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged += HandleValueChanged;

        // Reset to the top of the board when enabled
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    private void OnDisable()
    {
        // Stop listener
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").ValueChanged -= HandleValueChanged;

        DestroyChildren();
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
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
                id: ChildSnapshot.Key,
                rank: i,
                displayName: ChildSnapshot.Child("name").Value.ToString(),
                score: ChildSnapshot.Child("score").Value.ToString()
                );

            if (ChildSnapshot.Key == AuthController.UID)
            {
                userScore = entry.GetComponent<RectTransform>();
            }

            i++;
        }

        if (userScore != null)
        {
            SnapTo(userScore);
        }

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


