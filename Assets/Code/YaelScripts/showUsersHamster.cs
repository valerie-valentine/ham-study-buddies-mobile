using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;


public class showUsersHamster : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseUser currentUser;

    public GameObject[] hamsters;

    public static showUsersHamster instance;

    //whenever we LOAD the main page,
    //we only want the hamster with a matching name to the users hamster to appear


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        db = FirebaseFirestore.DefaultInstance;
        currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

    }

    void Start()
    {

        GetHamster();
    }



    public void ShowUsersHamster(int hamsterIndex)
    {
        if (hamsterIndex < 0 || hamsterIndex >= hamsters.Length)
        {
            Debug.LogWarning("Invalid hamster index!");
            return;
        }

        for (int i = 0; i < hamsters.Length; i++)
        {
            if (i == hamsterIndex)
                hamsters[i].SetActive(true);
            else
                hamsters[i].SetActive(false);
        }
    }




        //we want to get current user by their id
        //and get the string name of their hamster
        //return string of the hamsters name and use it to validate hamster?
        internal void GetHamster()
        {

            DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string hamsterName = snapshot.GetValue<string>("hamster");
                    Debug.Log("Hamster Name: " + hamsterName);
                }
                else
                {
                    Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
                }
            });

    }
}
