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
        //AddDataToFirestore();
        //UpdateDataFirestore();
        //DeleteDocFirestore();
        DeleteFieldFirestore();
    }

    //void AddDataToFirestore()
    //{
    //    // Your Firestore data insertion code here...
    //    DocumentReference docRef = db.Collection("Pokemon").Document("Squirtle");

    //    Dictionary<string, object> user = new Dictionary<string, object>
    //    {
    //        { "Type", "Water" },
    //        { "Move", "Watergun" },
    //        {"Level", 92 }
    //    };

    //    docRef.SetAsync(user).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("Added data to the alovelace document in the users collection.");
    //        }
    //        else if (task.IsFaulted)
    //        {
    //            Debug.LogError("Error adding data to Firestore: " + task.Exception);
    //        }
    //    });
    //}

    //void UpdateDataFirestore()
    //{
    //    DocumentReference docRef = db.Collection("Pokemon").Document("Pikachu");

    //    Dictionary<string, object> update = new Dictionary<string, object>
    //    {
    //        { "Trainer", "Valerie" }
    //    };
    //    docRef.SetAsync(update, SetOptions.MergeAll);
    //}

    //void DeleteDocFirestore()
    //{
    //    DocumentReference docRef = db.Collection("Pokemon").Document("Charmander");
    //    docRef.DeleteAsync();
    //}

    void DeleteFieldFirestore()
    {
        DocumentReference docRef = db.Collection("Pokemon").Document("Pikachu");

        Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "Trainer", FieldValue.Delete }

        };
        docRef.SetAsync(update, SetOptions.MergeAll);
    }

}
