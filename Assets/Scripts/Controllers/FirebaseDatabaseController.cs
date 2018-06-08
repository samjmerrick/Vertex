using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseDatabaseController : MonoBehaviour {

    public static DatabaseReference db;

    void Start()
    {
        Debug.Log("changed db reference");

        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://star-defender.firebaseio.com/");

        // Get the root reference location of the database.
        db = FirebaseDatabase.DefaultInstance.RootReference;

        
    }
}
