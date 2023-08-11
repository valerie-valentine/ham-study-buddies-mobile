using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PomoTimer : MonoBehaviour
{
    public float timeValue;
    public TMP_Text timeText;
    public bool timerActive = false;
    public Button increaseButton;
    public Button decreaseButton;
    public Button StartButton;
    public Button StopButton;
    public GameObject Continue;
    public GameObject Quit;
    public GameObject blushies;
    public float currency;
    public CurrencyManager currencyManager;
    public GameObject seedInfoDisplay;
    public GameObject stopConfirmation;
    public TMP_Text seedText;



    public void Awake()
    {
        //Must instantiate a new instance of CurrencyManager to be able to use in another script
       
    }

    void Start()
    {
        timeValue = 0;
        blushies.SetActive(false);
        seedInfoDisplay.SetActive(false);
        stopConfirmation.SetActive(false);
        Continue.SetActive(false);
        Quit.SetActive(false);
    }

    void Update()
    {
        if (timerActive == true) {
            StartButton.enabled = false;

            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                blushies.SetActive(true);
            }
            else
            {
                timeValue = 0;

            }
            Display(timeValue);

            if (timeValue < 6)
            {
                StopButton.enabled = false;
            }
            else
            {
                StopButton.enabled = true;
            }

            if (timeValue == 0)
            {
                timerActive = false;
                increaseButton.enabled = true;
                decreaseButton.enabled = true;
            }
        }
        else
        {
            StartButton.enabled = true;
            blushies.SetActive(false);
            StopButton.enabled = false;
        }
    }

    void Display(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    //public async void StartTimer()
    //{
    //    var currencyManager = CurrencyManager.instance;
    //    timerActive = true;
    //    increaseButton.enabled = false;
    //    decreaseButton.enabled = false;
    //    currency = timeValue / 300;


    //    float? currentBank = await currencyManager.GetCurrency();
    //    currencyManager.UpdateCurrency(currentBank.Value + currency);

    //    //StartCoroutine(ShowAndHideSeedInfo());

    //}

    public void StartTimer()
    {
        var moneyDisplay = MoneyDisplay.instance;
        var currencyManager = CurrencyManager.instance;
        timerActive = true;
        increaseButton.enabled = false;
        decreaseButton.enabled = false;
        currency = timeValue / 300;
        ShowSeedInfo();

        currencyManager.GetCurrency().ContinueWith(task =>
        {
            if (task.IsCompleted && task.Result.HasValue)
            {
                float? currentBank = task.Result;
                currencyManager.UpdateCurrency(currentBank.Value + currency);
            }
            else
            {
                Debug.LogError("Currency retrieval failed.");
            }
        });
    }


    public void StopTimer()
    {
 
        stopConfirmation.SetActive(true);
        Continue.SetActive(true);
        Quit.SetActive(true);
    }

    public void ContinueTimer()
    {
        stopConfirmation.SetActive(false);
        Continue.SetActive(false);
        Quit.SetActive(false);
    }

    public async void QuitTimer()
    {
        var currencyManager = CurrencyManager.instance;
        timerActive = false;
        timeValue = 0;
        increaseButton.enabled = true;
        decreaseButton.enabled = true;
        Display(timeValue);

        float? currentBankAgain = await currencyManager.GetCurrency();
        currencyManager.UpdateCurrency(currentBankAgain.Value - currency);
        stopConfirmation.SetActive(false);
        seedInfoDisplay.SetActive(false);
        Continue.SetActive(false);
        Quit.SetActive(false);
    }
    

    public void IncreaseTime()
    {
        if (timeValue < 3600)
        {
            timeValue += 300;
            Display(timeValue);
        }
    }

    public void DecreaseTime()
    {
        if (timeValue > 0)
        {
            timeValue -= 300;
            Display(timeValue);
        }
    }

    public void ShowSeedInfo()
    {
        {
            seedText.SetText($"Sweet! You'll earn {currency} seeds for this task!");
            seedInfoDisplay.SetActive(true);
            Invoke(nameof(HideSeedInfo), 2f);
        }
    }

    private void HideSeedInfo()
    {
        seedInfoDisplay.SetActive(false);
    }

    //public IEnumerator ShowAndHideSeedInfo()
    //{
    //    Debug.Log(currency);
    //    seedText.SetText($"Sweet! You'll earn {currency} seeds for this task!");
    //    seedInfoDisplay.SetActive(true);

    //    yield return new WaitForSeconds(10); // Wait for the specified duration

    //    seedInfoDisplay.SetActive(false); // Hide the display after the duration
    //}

}



