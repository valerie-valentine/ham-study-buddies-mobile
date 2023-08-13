using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;


public class InventoryManager : MonoBehaviour
{
    FirebaseFirestore db;
    public float currency;
    public float? userBank;

    public static InventoryManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        db = FirebaseManager.instance.db;

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


    public async Task<List<string>> GetInventory()
    {
        var currentUser = UserManager.instance.User;

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
     
    public async void BuyItemDatabase(string name, string type, string subtype, int price)
    {
        var currencyManager = CurrencyManager.instance;
        var moneyDisplay = MoneyDisplay.instance;

        userBank = await currencyManager.GetCurrency();

        if (price > userBank)
        {
            Debug.Log("You don't got monaaayyyy");
        }
        else
        {
            float? updatedBank = userBank - price;
            await currencyManager.UpdateCurrency(updatedBank);
            AddItemUserInventory(name, type, subtype);
            moneyDisplay.DisplayCurrency();
            //call display currency so it changes money amount after purchase
        }
    }

    public void AddItemUserInventory(string name, string type, string subtype)
    {
        var currentUser = UserManager.instance.User;
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory").Document();

        Dictionary<string, object> item = new Dictionary<string, object>
        {
            { "name", name },
            { "type", type },
            {"subtype", subtype},
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

    public void EquipUserItem(string name, string subtype)
    {
        var currentUser = UserManager.instance.User;
        CollectionReference inventoryRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
        Query query = inventoryRef.WhereEqualTo("subtype", subtype);

        query.GetSnapshotAsync().ContinueWithOnMainThread(querySnapshotTask =>
        {
            if (querySnapshotTask.IsCompleted)
            {
                foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
                {
                    DocumentReference docToUpdateRef = documentSnapshot.Reference;
                    Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

                    string itemName = (string)documentData["name"];
                    bool equippedStatus = (bool)documentData["equipped"];

                    if (itemName == name)
                    {
                        equippedStatus = !equippedStatus; // Toggle the equipped status for the specified item
                    }
                    else
                    {
                        equippedStatus = false; // Set equipped to false for all other items
                    }

                    Dictionary<string, object> updatedValue = new Dictionary<string, object>
                {
                    { "equipped", equippedStatus }
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

    public async Task<bool> GetEquippedStatus(string name)
    {
        var currentUser = UserManager.instance.User;
        CollectionReference inventoryRef = db.Collection("Users").Document(currentUser.UserId).Collection("inventory");
        Query query = inventoryRef.WhereEqualTo("name", name);
        QuerySnapshot inventoryQuerySnapshot = await query.GetSnapshotAsync();

        foreach (DocumentSnapshot documentSnapshot in inventoryQuerySnapshot.Documents)
        {
            Dictionary<string, object> documentData = documentSnapshot.ToDictionary();

            if (documentData.TryGetValue("equipped", out object equippedObj) && equippedObj is bool equippedStatus)
            {
                return equippedStatus;
            }
        }

        return false;

    }

}

