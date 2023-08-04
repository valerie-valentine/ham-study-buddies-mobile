using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class UpdateInventory : MonoBehaviour
{
    // Declare the FirebaseFirestore instance once in your script
    FirebaseFirestore db;

    void Awake()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;
        //FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //UpdateHamsterToUser(name);
        //GetHamster();
    }

    //void GetHamster()
    //{
    //    // GET from hamster
    //    // think this "route" needs an input based off of which you click
    //    // put the hamster or something in each button
    //    // by name (or by id, by index)
    //    // we want to return the string
    //    DocumentReference hamstersDocRef = db.Collection("Hamsters").Document("jasper");
    //    hamstersDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //    {
    //        DocumentSnapshot snapshot = task.Result;
    //        if (snapshot.Exists)
    //        {
    //            Debug.Log(System.String.Format("Document data for {0} document:", snapshot.Id));
    //            Dictionary<string, object> city = snapshot.ToDictionary();
    //            foreach (KeyValuePair<string, object> pair in city)
    //            {
    //                Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
    //        }
    //    });
    //}

    // Update to User
    // name string input here
    void UpdateHamsterToUser(string name)
    {
        // GET from hamster ( we need to put in a function call
        // UPDATE to user
        // think this route needs the returned value from GetHamster()

        DocumentReference usersDocRef = db.Collection("Users").Document("test");

        Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "hamster", name }
            };
        usersDocRef.SetAsync(update, SetOptions.MergeAll);
        Debug.Log("Hamster has been updated.");
    }
}

