using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // Declare the FirebaseFirestore instance once in your script
    FirebaseFirestore db;

    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;
    }

    void Start()
    {
        // Call the method to add data to Firestore
        AddDataToFirestore();
    }

    void AddDataToFirestore()
    {
        // Your Firestore data insertion code here...
        DocumentReference docRef = db.Collection("Pokemon").Document("Pikachu");

        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "Type", "Electric" },
            { "move", "tackle" },
        };

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Added data to the alovelace document in the users collection.");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });
    }
}