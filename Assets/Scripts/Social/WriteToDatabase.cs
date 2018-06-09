using UnityEngine;

public class WriteToDatabase {

    public static void NewHiScore(int _score)
    {
        Firebase.Auth.FirebaseUser user = UserManager.user;

        if (user == null) {
            Debug.Log("There is no signed in user");
            return;
        }


        writeNewScore(new Score
        {
            userID = user.UserId,
            profilePicture = user.PhotoUrl.ToString(),
            name = user.DisplayName,
            score = _score,
        });
    }

    private static void writeNewScore(Score score)
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

