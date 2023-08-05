using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using System.Threading.Tasks;


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
        ShowUsersHamster();
    }

   


    //conditionals, if name, get by index 

    public async void ShowUsersHamster()
    {
        string hamsterNameValue = await GetHamster();


        for (int i = 0; i < hamsters.Length; i++)
        {
            if (hamsters[i].name == hamsterNameValue)
                hamsters[i].SetActive(true);
            else
                hamsters[i].SetActive(false);
        }
    }





   //async function returning a task string , with async you need await 
    public async Task<string> GetHamster()
    {

        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        {

            if (snapshot.Exists)
            {
                string hamsterName = snapshot.GetValue<string>("hamster");
                Debug.Log("Hamster Name: " + hamsterName);
                return hamsterName;

            }
            else
            {
                Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
                return null;

            }
          
        }

    } 
 
}