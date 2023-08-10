using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;


public class InventoryManager : MonoBehaviour
{
    FirebaseFirestore db;
    public CurrencyManager currencyManager;
    public float currency;
    public float? userBank;
    //public AuthManager authManager;
    // Start is called before the first frame update
    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        currencyManager = FindObjectOfType<CurrencyManager>();

        Debug.Log("Starting Scene");
        //authManager = FindObjectOfType<AuthManager>();

    }

    // Update is called once per frame
    void Start()
    {
        //GetInventory();
    }

    public void GetInventory()
    {
        Debug.Log("In GetInventory function");
        Query allInventoryQuery = db.Collection("Users").Document("RIICyeIxCvTWSUaxHvbSXOkbbXY2").Collection("inventory");
        allInventoryQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allInventoryQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allInventoryQuerySnapshot.Documents)
            {
                Debug.Log($"Document data for {documentSnapshot.Id} document:");
                Dictionary<string, object> items = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in items)
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }

            }
        });
    }

    public async void ChargeUser(string inputString)
    {
        string delimiter = "_";
        List<string> stringList = new List<string>();
        string[] parts = inputString.Split(delimiter);
        stringList.AddRange(parts);

        string name = stringList[0];
        string type = stringList[1];
        int price = int.Parse(stringList[2]);

        userBank = await currencyManager.GetCurrency();
        if (price > userBank)
        {
            Debug.Log("You don't got monaaayyyy");
        }
        else
        {
            float? updatedBank = userBank - price;
            currencyManager.UpdateCurrency(updatedBank);
            AddItemUserInventory(name, type);
            // need to either call addfurniture function or addaccessories function
            // 
        }
    }


    public void AddItemUserInventory(string name, string type)
    {
        DocumentReference docRef = db.Collection("Users").Document("RIICyeIxCvTWSUaxHvbSXOkbbXY2").Collection("inventory").Document();

        Dictionary<string, object> item = new Dictionary<string, object>
        {
            { "name", name },
            { "type", type },
            {"equipped", false }
        };

        docRef.SetAsync(item).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Added {name} to User's inventory");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding data to Firestore: " + task.Exception);
            }
        });
    }





    //public void AddFurnitureUserDb(string name)
    //{

    //    // Your Firestore data insertion code here...
    //    DocumentReference docRef = db.Collection("Users").Document("RIICyeIxCvTWSUaxHvbSXOkbbXY2").Collection("inventory").Document();

    //    Dictionary<string, object> item = new Dictionary<string, object>
    //    {
    //        { "name", name },
    //        { "type", "furniture" },
    //        {"equipped", false }
    //    };

    //    docRef.SetAsync(item).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log($"Added {name} to User's inventory");
    //        }
    //        else if (task.IsFaulted)
    //        {
    //            Debug.LogError("Error adding data to Firestore: " + task.Exception);
    //        }
    //    });
    //}
    //Post-item
    //Post item to users inventory (item data - string: name, string: type)
    //We are going to hard code this data into UI
    //In our function we'll hard-code equipped to false

    //Buy-item
    //Params: (string: name, string: type, int: price)
    // call get currency
    // calculate the difference of item & userbank to see if user has enough
    // if statement - if user has enough currency call Post-item
    // call update currency
    // else - return message to user (NEED more seeds!!!)

    //public void AddAccessoriesUserDb(string name)
    //{

    //    // Your Firestore data insertion code here...
    //    DocumentReference docRef = db.Collection("Users").Document("RIICyeIxCvTWSUaxHvbSXOkbbXY2").Collection("inventory").Document();

    //    Dictionary<string, object> item = new Dictionary<string, object>
    //    {
    //        { "name", name },
    //        { "type", "accessories" },
    //        {"equipped", false }
    //    };

    //    docRef.SetAsync(item).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log($"Added {name} to User's inventory");
    //        }
    //        else if (task.IsFaulted)
    //        {
    //            Debug.LogError("Error adding data to Firestore: " + task.Exception);
    //        }
    //    });
}

