using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;


public class UpdateInventory : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseUser currentUser;

    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
    }


    public void UpdateHamsterToUser(string name)
    {
        if (currentUser == null)
        {
            Debug.LogError("User is not signed in!");
            return;
        }

        DocumentReference usersDocRef = db.Collection("Users").Document(currentUser.UserId);
        Debug.Log($"Current User: {currentUser}");

        Dictionary<string, object> update = new Dictionary<string, object>
            {
                { "hamster", name }
            };
        usersDocRef.SetAsync(update, SetOptions.MergeAll);
        Debug.Log($"Hamster {name} has been added to {currentUser.DisplayName}.");
    }

}