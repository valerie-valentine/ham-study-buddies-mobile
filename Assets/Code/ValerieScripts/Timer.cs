using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue;
    public TMP_Text timeText;
    public bool timerActive = false;
    public Button increaseButton;
    public Button decreaseButton;
    public Button StopButton;
    public float currency;


    void Start()
    {
        timeValue = 0;
    }

    void Update()
    {
        if (timerActive == true) {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
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
            Debug.Log(currency);
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

    public void StartTimer()
    {
        timerActive = true;
        increaseButton.enabled = false;
        decreaseButton.enabled = false;
        currency = timeValue / 300;

    }

    public void StopTimer()
    {
        
        timerActive = false;
        timeValue = 0;
        increaseButton.enabled = true;
        decreaseButton.enabled = true;
        Debug.Log(currency);
        Display(timeValue);
        currency = 0;
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

}
