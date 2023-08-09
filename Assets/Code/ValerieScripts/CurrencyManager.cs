using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;


public class CurrencyManager : MonoBehaviour
{
    FirebaseFirestore db;
    //FirebaseUser currentUser;

    public void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        //currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    void Start()
    {

        // Initialize Firebase and then start the timer
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfully initialized Firebase dependencies");
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase dependencies.");
            }
        });
    }

    public async Task<float?> GetCurrency()
    {
        DocumentReference docRef = db.Collection("Users").Document("dDmkJwTvo3H6ejthKf7i");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            float currentBank = snapshot.GetValue<float>("currency");
            Debug.Log("Current Bank: " + currentBank);
            return currentBank;
        }
        else
        {
            Debug.Log("Currency data not available or document does not exist.");
            return null;
        }
    }

    public void UpdateCurrency(float? money)
    {
        if (money == null)
        {
            Debug.Log("Currency data not available or document does not exist.");
        }
        else
        {
            DocumentReference docRef = db.Collection("Users").Document("dDmkJwTvo3H6ejthKf7i");

            Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "currency", money }
        };
            docRef.SetAsync(update, SetOptions.MergeAll);
        }
    }
}
