using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHamsterButton : MonoBehaviour
{
    public static UIHamsterButton instance;

    //Screen object variables
    public GameObject[] hamsterButtons;
    public GameObject[] hamsterProfileButtons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Show hamster profile (hamster face button stays visible)
    // Let's start with hamster 1
    // Only one profile button can be true at a time 
    public void ShowHamProfile(int hamsterIndex)
    {
        if (hamsterIndex < 0 || hamsterIndex >= hamsterProfileButtons.Length)
        {
            Debug.LogWarning("Invalid hamster index!");
            return;
        }

        for (int i = 0; i < hamsterProfileButtons.Length; i++)
        {
            if (i == hamsterIndex)
                hamsterProfileButtons[i].SetActive(true);
            else
                hamsterProfileButtons[i].SetActive(false);
        }
    }
}
