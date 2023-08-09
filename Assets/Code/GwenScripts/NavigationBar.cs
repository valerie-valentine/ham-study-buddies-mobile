using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//gwen, this code got edited because the nav windows were toggling on and offcorrectly, but they werent
//hiding the other windows, and notepad wouldnt hide, so we implemented the code from "pick a hamster" where we loop
//through indexes.



public class NavigationBar : MonoBehaviour
{
    public static NavigationBar instance;

    //public Button profileButton;
    //public Button shoppingButton;
    //public Button closetButton;
    //public Button furnitureButton;
    //public Button timerButton;
    //public Button notepadButton;

    // public GameObject profileWindow;
    // public GameObject shoppingWindow;
    // public GameObject closetWindow;
    // public GameObject furnitureWindow;
    // public GameObject timerWindow;
    //public GameObject notepadWindow;

    public GameObject[] navButtons;
    public GameObject[] navWindows;


    void Awake()
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

    public void ShowActiveWindow(int windowIndex)
    {

        for (int i = 0; i < navButtons.Length; i++)
        {
            if (i != windowIndex)
                navWindows[i].SetActive(false);
        }

        GameObject windowToToggle = navWindows[windowIndex];

        // Check if the window is active and toggle its state
        windowToToggle.SetActive(!windowToToggle.activeSelf);
    }


    //put our windows in here
    //    public void HideAllWindows()
    //    {
    //        profileWindow.SetActive(false);
    //        shoppingWindow.SetActive(false);
    //        closetWindow.SetActive(false);
    //        furnitureWindow.SetActive(false);
    //        timerWindow.SetActive(false);
    //        notepadWindow.SetActive(false);

    //    }

    //    public void showProfileWindow()
    //    {
    //        ShowWindow(profileWindow);
    //    }

    //    public void showShoppingWindow()
    //    {
    //        ShowWindow(shoppingWindow);
    //    }

    //    public void showClosetWindow()
    //    {
    //        ShowWindow(closetWindow);
    //    }

    //    public void showFurnitureWindow()
    //    {
    //        ShowWindow(furnitureWindow);
    //    }

    //    public void showTimerWindow()
    //    {
    //        ShowWindow(timerWindow);
    //    }

    //    public void showNotepadWindow()
    //    {
    //        ShowWindow(notepadWindow);
    //    }

    //    public void ShowWindow(GameObject window)
    //    {
    //        if(window.activeSelf)
    //        {
    //            HideAllWindows();
    //        } else {
    //            window.SetActive(true);
    //        }
    //    }

}

