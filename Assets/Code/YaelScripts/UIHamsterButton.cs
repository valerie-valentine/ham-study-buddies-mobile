//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIHamsterButton : MonoBehaviour
//{
//    public static UIHamsterButton instance;

//    //Screen object variables
//    public GameObject hamster1button;
//    public GameObject hamster2button;
//    public GameObject hamster3button;
//    public GameObject ham1profilebutton;
//    public GameObject ham2profilebutton;
//    public GameObject ham3profilebutton;



//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//        else if (instance != null)
//        {
//            Debug.Log("Instance already exists, destroying object!");
//            Destroy(this);
//        }
//    }

//    //show hamster profile (hamster face button stays visible)
//    //lets start with hamster 1
//    //only one profile button can be true at a time 
//    public void ShowHamProfile1()
//    {
//        ham1profilebutton.SetActive(true);
//        ham2profilebutton.SetActive(false);
//        ham3profilebutton.SetActive(false);

//    }

//    public void ShowHamProfile2()
//    {
//        ham1profilebutton.SetActive(false);
//        ham2profilebutton.SetActive(true);
//        ham3profilebutton.SetActive(false);

//    }

//    public void ShowHamProfile3()
//    {
//        ham1profilebutton.SetActive(false);
//        ham2profilebutton.SetActive(false);
//        ham3profilebutton.SetActive(true);

//    }

//}

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
