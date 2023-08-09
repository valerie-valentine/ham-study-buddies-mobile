using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// functions here that we attach to on click, toggle button on/off to equip and unequip
// when couch is equipped, no otehr furniture can be equipped with that room area
//same for clothes.
//toggle should use TMP to say item is equipped


//REFACTOR!!! establish a parameter that takes area of room/ body? (just using different functions for now)


public class UIEquipItems : MonoBehaviour
{

    public static UIEquipItems instance;

    //game objects for head, eyes, neck, hands , body
    //individual lists for body sections

    public Button[] headgear;
    public GameObject[] equippedHeadgear;

    public Button[] eyewear;
    public GameObject[] equippedEyewear;

    public Button[] neckwear;
    public GameObject[] equippedNeckwear;

    public Button[] handheld;
    public GameObject[] equippedHandheld;

    public Button[] body;
    public GameObject[] equippedBody;

    //object for couch, table, rug, decor

    public Button[] couches;
    public GameObject[] equippedCouch;

    public Button[] tables;
    public GameObject[] equippedTable;

    public Button[] rugs;
    public GameObject[] equippedRug;

    public Button[] decor;
    public GameObject[] equippedDecor;

    

    void Awake()
    {
        //instances go here
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

    //body equip by section, maybe theres a better way? this is making lists out of
    //accessories we dont want to overlap.

    public void EquipHeadgear(int Index)
    {
        if(equippedHeadgear[Index].activeSelf){
            headgear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedHeadgear[Index].SetActive(false);
    
        } else {

            for (int i = 0; i < headgear.Length; i++)
        {
            equippedHeadgear[i].SetActive(false);
            headgear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        //when i click BUTTON equippedHeadgear needs to toggle on and off
        GameObject itemToToggle = equippedHeadgear[Index];
        itemToToggle.SetActive(true); //toggling isnt happening
        headgear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";

    }






    // public void EquipEyewear(int Index)
    // {
    //     if(equippedEyewear[Index].activeSelf){
    //         eyewear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedEyewear[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < eyewear.Length; i++)
    //     {
    //         equippedEyewear[i].SetActive(false);
    //         eyewear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedEyewear[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     eyewear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }


    ////
    // public void EquipNeckwear(int Index)
    // {
    //     if(equippedNeckwear[Index].activeSelf){
    //         neckwear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedNeckwear[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < neckwear.Length; i++)
    //     {
    //         equippedNeckwear[i].SetActive(false);
    //         neckwear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedNeckwear[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     neckwear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";

    // }
    

    // public void EquipHandheld(int Index)
    // {
    //     if(equippedHandheld[Index].activeSelf){
    //         handheld[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedHandheld[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < handheld.Length; i++)
    //     {
    //         equippedHandheld[i].SetActive(false);
    //         handheld[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedHandheld[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     handheld[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }

    // public void EquipBody(int Index)
    // {
    //     if(equippedBody[Index].activeSelf){
    //         body[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedBody[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < body.Length; i++)
    //     {
    //         equippedBody[i].SetActive(false);
    //         body[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedBody[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     body[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }

    ////furniture equip by section

    // public void EquipCouches(int Index)
    // {
    //     if(equippedCouch[Index].activeSelf){
    //         couches[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedCouch[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < couches.Length; i++)
    //     {
    //         equippedCouch[i].SetActive(false);
    //         couches[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedCouch[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     couches[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }


    // public void EquipTables(int Index)
    // {
    //     if(equippedTable[Index].activeSelf){
    //         tables[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedTable[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < tables.Length; i++)
    //     {
    //         equippedTable[i].SetActive(false);
    //         tables[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedTable[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     tables[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }


    // public void EquipRugs(int Index)
    // {
    //     if(equippedRug[Index].activeSelf){
    //         rugs[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedRug[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < rugs.Length; i++)
    //     {
    //         equippedRug[i].SetActive(false);
    //         rugs[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedRug[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     rugs[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }


    // public void EquipDecor(int Index)
    // {
    //     if(equippedDecor[Index].activeSelf){
    //         decor[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //         equippedDecor[Index].SetActive(false);
    
    //     } else {

    //         for (int i = 0; i < decor.Length; i++)
    //     {
    //         equippedDecor[i].SetActive(false);
    //         decor[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //     }

    //     //when i click BUTTON equippedHeadgear needs to toggle on and off
    //     GameObject itemToToggle = equippedDecor[Index];
    //     itemToToggle.SetActive(true); //toggling isnt happening
    //     decor[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    // }


    }
}
