using UnityEngine;

public class SaveScore : MonoBehaviour {

    private void OnEnable()
    {
        GameController.GameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameController.GameEnd -= EndGame;
    }

    void EndGame()
    {
        Firebase.Auth.FirebaseUser user = FirebaseUser.user;

        if (user == null) {
            Debug.Log("There is no signed in user");
            return;
        }


        writeNewScore(new Score
        {
            userID = user.UserId,
            profilePicture = user.PhotoUrl.ToString(),
            name = user.DisplayName,
            score = Stats.gameStats["Destroyed"],
        });
    }

    private void writeNewScore(Score score)
    {
        string json = JsonUtility.ToJson(score);
        FirebaseDatabaseController.db.Child("scores").Child(score.userID).SetRawJsonValueAsync(json);

        Debug.Log("Wrote new score" + json);
    }

    public class Score
    {
        public string userID;
        public string profilePicture;
        public int score;
        public string name;
    }

}

