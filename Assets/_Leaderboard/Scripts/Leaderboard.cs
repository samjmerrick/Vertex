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

    private void OnEnable()
    {
        // Reset to the top of the board when enabled
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        ReadOnceFromDatabase();
    }

    private void OnDisable()
    {
        DestroyChildren();
        
        if(_LoadingSymbol != null)
        {   
            Destroy(_LoadingSymbol);
        } 
    }

    private async void ReadOnceFromDatabase()
    {
        // Instantiate the loading Symbol
        _LoadingSymbol = Instantiate(LoadingSymbol, LeaderboardContent.transform.parent);

        DataSnapshot snapshot = await FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").GetValueAsync();

        if (snapshot == null) return;

        // i is used as Rank
        int i = 1;

        // Reverse loop gives descending order
        foreach (var ChildSnapshot in snapshot.Children.Reverse())
        {
            LeaderboardEntry entry;

            if (ChildSnapshot.Key == AuthController.UID)
            {
                entry = Instantiate(MyLeaderboardEntry, LeaderboardContent.transform).GetComponent<LeaderboardEntry>();
                userScore = entry.GetComponent<RectTransform>();
            }
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


