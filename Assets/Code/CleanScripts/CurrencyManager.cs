using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;
using TMPro;



public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    FirebaseFirestore db;

    public void Awake()
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

    void Start()
    {
        _ = GetCurrency();
     
    }

    public async Task<float?> GetCurrency()
    {
        var currentUser = UserManager.instance.User;
        DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        

        if (snapshot.Exists)
        {
            float currentBank = snapshot.GetValue<float>("money");
            Debug.Log("Current Bank: " + currentBank);
            return currentBank;
            
        }
        else
        {
            Debug.Log("Currency data not available or document does not exist.");
            return null;
        }
    }

    public async Task UpdateCurrency(float? money)
    {
        var currentUser = UserManager.instance.User;
        if (money == null)
        {
            Debug.Log("Currency data not available or document does not exist.");
        }
        else
        {
            DocumentReference docRef = db.Collection("Users").Document(currentUser.UserId);

            Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "money", money }
        };
            await docRef.SetAsync(update, SetOptions.MergeAll);

            string moneyStr = money.Value.ToString();
          
        }
    }
}
