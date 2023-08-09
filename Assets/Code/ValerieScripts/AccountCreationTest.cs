using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;

public class AccountCreationTest : MonoBehaviour
{
    FirebaseFirestore db;
    // Start is called before the first frame update
    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    void Start()
    {
        _ = CreateNewUserAsync();
    }


    //public async Task CreateNewUserAsync()
    //{
    //    DocumentReference docRef = db.Collection("Users").Document();

    //    Dictionary<string, object> user = new Dictionary<string, object>
    //    {
    //        {"username", "Luis"},
    //        { "money", 0 },
    //        { "hamster", "" }
    //    };

    //    await docRef.SetAsync(user);

    //    Debug.Log("Added new user to the users collection.");

    //    CollectionReference inventoryCollectionRef = docRef.Collection("inventory");

    //    DocumentReference inventoryDocRef = inventoryCollectionRef.Document();
    //    await inventoryDocRef.SetAsync(new Dictionary<string, object>());

    //    Debug.Log("Created inventory subcollection under user document.");
    //}



    //public async Task CreateNewUserAsync()
    //{
    //    DocumentReference docRef = db.Collection("Users").Document();

    //    Dictionary<string, object> user = new Dictionary<string, object>
    //{
    //    {"username", "Gwen"},
    //    { "money", 0 },
    //    { "hamster", "" }
    //};

    //    await docRef.SetAsync(user);

    //    Debug.Log("Added new user to the users collection.");

    //    // After setting the user data, create the "inventory" subcollection
    //    CollectionReference inventoryCollectionRef = docRef.Collection("inventory");
    //    Dictionary<string, object> initialInventoryData = new Dictionary<string, object>
    //{
    //    {"item", "item_name"},
    //    {"quantity", 10}
    //};
    //    await inventoryCollectionRef.AddAsync(initialInventoryData);

    //    Debug.Log("Created inventory subcollection under user document.");
    //}



    //To add multiple Items
    public async Task CreateNewUserAsync()
    {
        DocumentReference docRef = db.Collection("Users").Document();

        Dictionary<string, object> user = new Dictionary<string, object>
    {
        {"username", "Bingus"},
        { "money", 0 },
        { "hamster", "" }
    };

        await docRef.SetAsync(user);

        Debug.Log("Added new user to the users collection.");

        // After setting the user data, create the "inventory" subcollection
        CollectionReference inventoryCollectionRef = docRef.Collection("inventory");

        // Create an array of inventory data (example data)
        List<Dictionary<string, object>> inventoryDataList = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            {"item", "couch"},
            {"quantity", 5}
        },
        new Dictionary<string, object>
        {
            {"item", "hat"},
            {"quantity", 8}
        },
        // Add more items as needed
    };

        // Add each inventory item to the subcollection
        foreach (var inventoryData in inventoryDataList)
        {
            await inventoryCollectionRef.AddAsync(inventoryData);
        }


    }
}