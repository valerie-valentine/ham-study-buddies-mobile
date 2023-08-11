using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using Firebase.Auth;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay instance;
    FirebaseUser currentUser;
    public CurrencyManager currencyManager;
    public TMP_Text currencyText;


    void Awake()
    {
        instance = this;
        currentUser = AuthManager.instance.User;
        currencyManager = CurrencyManager.instance;
        DisplayCurrency();


    }


     void Start()
    {
     DisplayCurrency();
    }

    public async void DisplayCurrency()
    {

        var moneyStr = await currencyManager.GetCurrency();
        currencyText.SetText(moneyStr.ToString());


    }
}


