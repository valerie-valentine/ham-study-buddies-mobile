using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NavigationBar : MonoBehaviour
{

    public Button profileButton;
    public Button shoppingButton;
    public Button closetButton;
    public Button furnitureButton;
    public Button timerButton;
    public Button notepadButton;

    // public GameObject profileWindow;
    // public GameObject shoppingWindow;
    // public GameObject closetWindow;
    // public GameObject furnitureWindow;
    // public GameObject timerWindow;
    public GameObject notepadWindow;


    void Awake(){
        HideAllWindows();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //put our windows in here
    public void HideAllWindows()
    {
        // profileWindow.SetActive(false);
        // shoppingWindow.SetActive(false);
        // closetWindow.SetActive(false);
        // furnitureWindow.SetActive(false);
        // timerWindow.SetActive(false);
        notepadWindow.SetActive(false);

    }   

    // public void showProfileWindow(){
    //     ShowWindow(profileWindow);
    // }

    // public void showShoppingWindow(){
    //     ShowWindow(shoppingWindow);
    // }

    // public void showClosetWindow(){
    //     ShowWindow(closetWindow);
    // }

    // public void showFurnitureWindow(){
    //     ShowWindow(furnitureWindow);
    // }

    // public void showTimerWindow(){
    //     ShowWindow(timerWindow);
    // }

    public void showNotepadWindow(){
        ShowWindow(notepadWindow);
    }
    
    public void ShowWindow(GameObject window){
        if(window.activeSelf){
            HideAllWindows();
        } else {
            window.SetActive(true);
        }
    }

}

