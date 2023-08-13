using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay instance;
    public CurrencyManager currencyManager;
    public TMP_Text currencyText;
    float? money;


    void Awake()
    {
        instance = this;
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


