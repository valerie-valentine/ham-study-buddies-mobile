using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIEquipItems : MonoBehaviour
{

    public static UIEquipItems instance;
    public InventoryManager inventoryManager;
    AudioManager audioManager;


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

    //objects for couch, table, rug, decor

    public Button[] decor;
    public GameObject[] equippedDecor;

    public Button[] tables;
    public GameObject[] equippedTable;

    public Button[] couches;
    public GameObject[] equippedCouch;

    public Button[] rugs;
    public GameObject[] equippedRug;

    public Button[] wallpapers;
    public GameObject[] equippedWallpaper;

    public Button[] curtains;
    public GameObject[] equippedCurtain;

    public Button[] ceiling;
    public GameObject[] equippedCeiling;




    // item == name
    // item == dictionay[name]{


    void Awake()
    {
        inventoryManager = InventoryManager.instance;
        //await inventoryManager.GetEquippedStatus();

        EquipItemDisplay(equippedHeadgear, headgear);
        EquipItemDisplay(equippedEyewear, eyewear);
        EquipItemDisplay(equippedNeckwear, neckwear);
        EquipItemDisplay(equippedHandheld, handheld);
        EquipItemDisplay(equippedBody, body);

        EquipItemDisplay(equippedDecor, decor);
        EquipItemDisplay(equippedTable, tables);
        EquipItemDisplay(equippedCouch, couches);
        EquipItemDisplay(equippedRug, rugs);
        EquipItemDisplay(equippedWallpaper, wallpapers);

        EquipItemDisplay(equippedCurtain, curtains);
        EquipItemDisplay(equippedCeiling, ceiling);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //instances go here
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
       }
    }

    //this should help show the ui deepending on the database!
    public async void EquipItemDisplay(GameObject[] equippedItems, Button[] ownedItems)
    {
        for (int index = 0; index < equippedItems.Length; index++)
        {
            //this gets the name from our UI
            string itemName = equippedItems[index].name.Replace("Display", "");
            //this gets if the item is true or false in our database
            bool equippedStatus = await inventoryManager.GetEquippedStatus(itemName);


            //this sets the item in UI to be active depending on the status in the database
            equippedItems[index].SetActive(equippedStatus);
            ownedItems[index].GetComponentInChildren<TextMeshProUGUI>().text = equippedStatus ? "Equipped" : "";
        }

        //contains for equipped itemslist

    }


    //Sets sets sprite to text to equipped, sets all other same type of room sprites to inactive in list, and set correct sprite to active & toggles 

    //this is a helper function we can apply to all these dang lists ! 
    public void EquipItemHelper(Button[] ownedItem, GameObject[] equippedItem, int index, string subtype)
    {
        string itemName = ownedItem[index].name.Replace("Display", "");
        //subtype has to equal
        inventoryManager.EquipUserItem(itemName, subtype);
        if (equippedItem[index].activeSelf)

        {
            ownedItem[index].GetComponentInChildren<TextMeshProUGUI>().text = itemName;
            equippedItem[index].SetActive(false);
        }
        else
        {
            for (int i = 0; i < ownedItem.Length; i++)
            {
                equippedItem[i].SetActive(false);
                ownedItem[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }


            GameObject itemToToggle = equippedItem[index];
            itemToToggle.SetActive(true);
            ownedItem[index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }
    }



    public void EquipHeadgear(int Index)
    {
       EquipItemHelper(headgear, equippedHeadgear, Index, "headgear");
       audioManager.PlaySFX(audioManager.deleteSFX);
    }

    
    public void EquipEyewear(int Index)
    {
        EquipItemHelper(eyewear, equippedEyewear, Index, "eyewear");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipNeckwear(int Index)
    {
        EquipItemHelper(neckwear, equippedNeckwear, Index, "neckwear");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipHandheld(int Index)
    {
      EquipItemHelper(handheld, equippedHandheld, Index, "handheld");
      audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipBody(int Index)
    {
       EquipItemHelper(body, equippedBody, Index, "body");
       audioManager.PlaySFX(audioManager.deleteSFX);
    }

    ////furniture equip by section

    public void EquipDecor(int Index)
    {
      EquipItemHelper(decor, equippedDecor, Index, "decor");
      audioManager.PlaySFX(audioManager.deleteSFX);
    }

    public void EquipTables(int Index)
    {
        EquipItemHelper(tables, equippedTable, Index, "table");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipCouches(int Index)
    {
      EquipItemHelper(couches, equippedCouch, Index, "couch");
      audioManager.PlaySFX(audioManager.deleteSFX);

    }


    public void EquipRugs(int Index)
    {
        EquipItemHelper(rugs, equippedRug, Index, "rug");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipWallpapers(int Index)
    {
        EquipItemHelper(wallpapers, equippedWallpaper, Index, "wallpaper");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }

    public void EquipCurtains(int Index)
    {
        EquipItemHelper(curtains, equippedCurtain, Index, "curtain");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


    public void EquipCeiling(int Index)
    {
        EquipItemHelper(ceiling, equippedCeiling, Index, "ceiling");
        audioManager.PlaySFX(audioManager.deleteSFX);
    }


}

