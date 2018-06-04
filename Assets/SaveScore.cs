using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SaveScore : MonoBehaviour {

    DatabaseReference mDatabase;

    private void OnEnable()
    {
        GameController.GameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameController.GameEnd -= EndGame;
    }

    // Use this for initialization
    void Start ()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://star-defender.firebaseio.com/");

        // Get the root reference location of the database.
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void EndGame()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        string userId = "xxxxxxxx";
        string name = "Sam Merrick";
        int score = Stats.gameStats["Destroyed"];

        writeNewScore(userId, name, score);
    }

    public class Score
    {
        public string userID;
        public int score;
        public string name;
    }

    private void writeNewScore(string _userID, string _name, int _score)
    {
        Score score = new Score
        {
            userID = _userID,
            name = _name,
            score = _score
        };

        string json = JsonUtility.ToJson(score);
        Debug.Log(json);

        mDatabase.Child("scores").Child(_userID).SetRawJsonValueAsync(json);

    }
}
