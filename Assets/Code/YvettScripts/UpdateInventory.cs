using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class UpdateInventory : MonoBehaviour
{
    // Declare the FirebaseFirestore instance once in your script
    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the FirebaseFirestore instance in the Awake method
        db = FirebaseFirestore.DefaultInstance;
        //FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHamsterToUser();
        GetHamster();
    }

    // Update to User
    void UpdateHamsterToUser()
    {
        // GET from hamster ( we need to put in a function call
        // UPDATE to user
        // think this route needs the returned value from GetHamster()

        DocumentReference usersDocRef = db.Collection("Hamsters").Document("jasper");

        Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "favColor", "blue" } // Hamster.Name?
            };
        usersDocRef.SetAsync(update, SetOptions.MergeAll);
        Debug.Log("Hamster has been updated.");
    }

    // do I have to destroy the object immediately?
    void GetHamster()
    {
        // GET from hamster
        // think this "route" needs an input
        DocumentReference hamstersDocRef = db.Collection("Hamsters").Document("jasper");
        hamstersDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(System.String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
}
