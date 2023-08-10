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
    FirebaseUser currentUser;
    public CurrencyManager currencyManager;
    public float currency;
    public float? userBank;


    // Start is called before the first frame update
    void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        currencyManager = CurrencyManager.instance;
        currentUser = AuthManager.instance.User;

    }

    // Update is called once per frame
    void Start()
    {
        //GetInventory();
    }

    public void GetInventory()
    {

        Query allInventoryQuery = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
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

    public async void BuyItemDatabase(string inputString)
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
        }
    }

    public void AddItemUserInventory(string name, string type)
    {
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

    public void EquipUserItem(string name)
    {
        CollectionReference inventoryRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
        Query query = inventoryRef.WhereEqualTo("name", name);
        query.GetSnapshotAsync().ContinueWithOnMainThread(querySnapshotTask =>
        {
            if (querySnapshotTask.IsCompleted)
            {
                foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
                {
                    DocumentReference docToUpdateRef = documentSnapshot.Reference;
                    Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

                    bool equippedStatus = (bool)documentData["equipped"];

                    Dictionary<string, object> updatedValue = new Dictionary<string, object>
            {
                { "equipped", !equippedStatus }
            };

                    docToUpdateRef.UpdateAsync(updatedValue).ContinueWithOnMainThread(updateTask =>
                    {
                        if (updateTask.IsCompleted)
                        {
                            Debug.Log($"Document {docToUpdateRef.Id} updated successfully.");
                        }
                        else if (updateTask.IsFaulted)
                        {
                            Debug.LogError($"Error updating document {docToUpdateRef.Id}: {updateTask.Exception}");
                        }
                    });
                }
            }
            else if (querySnapshotTask.IsFaulted)
            {
                Debug.LogError($"Error querying documents: {querySnapshotTask.Exception}");
            }
        });
    }

}

