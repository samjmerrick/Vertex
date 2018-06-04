using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SaveScore : MonoBehaviour {

    DatabaseReference mDatabase;

	// Use this for initialization
	void Start ()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://star-defender.firebaseio.com/");

        // Get the root reference location of the database.
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public class User
    {
        public string username;
        public string email;

        public User()
        {
        }

        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }

    private void writeNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        mDatabase.Child("scores").Child(userId).SetRawJsonValueAsync(json);
    }
}
