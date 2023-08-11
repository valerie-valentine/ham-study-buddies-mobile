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
    float? money;


    void Awake()
    {
        instance = this;
        currentUser = AuthManager.instance.User;
        currencyManager = CurrencyManager.instance;
        DisplayCurrency();


    }

    private void Update()
    {
        currencyText.text = money.ToString(); 
    }


    void Start()
    {
     DisplayCurrency();
    }

    public async void DisplayCurrency()
    {

        var moneyValue = await currencyManager.GetCurrency();
        money = moneyValue;
        Debug.Log(moneyValue);
    }

 
}


