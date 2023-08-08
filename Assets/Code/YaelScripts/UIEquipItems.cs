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
        for (int i = 0; i < headgear.Length; i++)
        {
            if (i != Index)
            {
                equippedHeadgear[i].SetActive(false);
            }
            else
            {
                equippedHeadgear[i].SetActive(true);
                headgear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped"; //this isnt working
            }

        }

        //when i click BUTTON equippedHeadgear needs to toggle on and off
        GameObject itemToToggle = equippedHeadgear[Index];
        itemToToggle.SetActive(!itemToToggle.activeSelf); //toggling isnt happening


    }





    //public void EquipEyewear(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}


    ////
    //public void EquipNeckwear(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}

    //public void EquipHandheld(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}

    //public void EquipBody(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}

    ////furniture equip by section

    //public void EquipCouches(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}


    //public void EquipTables(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}


    //public void EquipRugs(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == FurnitureIndex)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}


    //public void EquipDecor(int Index)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == Index)
    //            ownedFurniture[i].SetActive(true);
    //        shoppingFurniture[Index].interactable = false;


    //        shoppingFurniture[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";


    //    }
    //}



}
