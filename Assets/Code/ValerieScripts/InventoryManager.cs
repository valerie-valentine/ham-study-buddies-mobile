using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;


public class InventoryManager : MonoBehaviour
{
    FirebaseFirestore db;
    public CurrencyManager currencyManager;
    public float currency;
    public float? userBank;

    public static InventoryManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        currencyManager = CurrencyManager.instance;


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log($"CurrencyManager {gameObject.GetInstanceID()} has been DESTROYED");
            return;
        }


    }

    // Update is called once per frame
    void Start()
    {
        
    }

    public async Task<List<string>> GetInventory()
    {
        var currentUser = AuthManager.instance.User;
        List<string> namesList = new List<string>();
        Query allInventoryQuery = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
        QuerySnapshot allInventoryQuerySnapshot = await allInventoryQuery.GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in allInventoryQuerySnapshot.Documents)
        {
            Debug.Log($"Document data for {documentSnapshot.Id} document:");
            Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

            if (documentData.ContainsKey("name"))
            {
                object nameValue = documentData["name"];
                if (nameValue is string name)
                {
                    Debug.Log($"Name: {name}");
                    namesList.Add(name);
                }
            }
        }

        return namesList;
    }

    public async void BuyItemDatabase(string name, string type, int price)
    {
        //var currentUser = AuthManager.instance.User;
        var moneyDisplay = MoneyDisplay.instance;

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
            moneyDisplay.DisplayCurrency();
            //call display currency so it changes money amount after purchase
        }
    }

    public void AddItemUserInventory(string name, string type)
    {
        var currentUser = AuthManager.instance.User;
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory").Document();

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

    //public void EquipUserItem(string name)
    //{
    //    var currentUser = AuthManager.instance.User;
    //    CollectionReference inventoryRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
    //    Query query = inventoryRef.WhereEqualTo("name", name);
    //    query.GetSnapshotAsync().ContinueWithOnMainThread(querySnapshotTask =>
    //    {
    //        if (querySnapshotTask.IsCompleted)
    //        {
    //            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
    //            {
    //                DocumentReference docToUpdateRef = documentSnapshot.Reference;
    //                Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

    //                bool equippedStatus = (bool)documentData["equipped"];

    //                Dictionary<string, object> updatedValue = new Dictionary<string, object>
    //        {
    //            { "equipped", !equippedStatus }
    //        };

    //                docToUpdateRef.UpdateAsync(updatedValue).ContinueWithOnMainThread(updateTask =>
    //                {
    //                    if (updateTask.IsCompleted)
    //                    {
    //                        Debug.Log($"Document {docToUpdateRef.Id} updated successfully.");
    //                    }
    //                    else if (updateTask.IsFaulted)
    //                    {
    //                        Debug.LogError($"Error updating document {docToUpdateRef.Id}: {updateTask.Exception}");
    //                    }
    //                });
    //            }
    //        }
    //        else if (querySnapshotTask.IsFaulted)
    //        {
    //            Debug.LogError($"Error querying documents: {querySnapshotTask.Exception}");
    //        }
    //    });
    //}

    public async Task<bool> EquipUserItem(string name)
    {
        var currentUser = AuthManager.instance.User;
        CollectionReference inventoryRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
        Query query = inventoryRef.WhereEqualTo("name", name);

        var querySnapshot = await query.GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            DocumentReference docToUpdateRef = documentSnapshot.Reference;
            Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

            bool equippedStatus = (bool)documentData["equipped"];

            Dictionary<string, object> updatedValue = new Dictionary<string, object>
        {
            { "equipped", !equippedStatus }
        };

            await docToUpdateRef.UpdateAsync(updatedValue);

            return equippedStatus; // Return the updated equipped status
        }

        return false; // Return false if the item was not found
    }


}

