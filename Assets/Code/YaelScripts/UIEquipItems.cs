using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIEquipItems : MonoBehaviour
{

    public static UIEquipItems instance;
    public InventoryManager inventoryManager;


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

    public Button[] couches;
    public GameObject[] equippedCouch;

    public Button[] tables;
    public GameObject[] equippedTable;

    public Button[] rugs;
    public GameObject[] equippedRug;

    public Button[] wallpapers;
    public GameObject[] equippedWallpaper;




    void Awake()
    {
        inventoryManager = InventoryManager.instance;
        //EquipItemDisplay(equippedDecor, decor);

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
    //public  void EquipItemDisplay(GameObject[] equippedItems, Button[] ownedItems)
    //{
    //    for (int index = 0; index < equippedItems.Length; index++)
    //    {
    //        //this gets the name from our UI
    //        string itemName = equippedItems[index].name.Replace("Display", "");
    //        //this gets if the item is true or false in our database
    //        bool equippedStatus = inventoryManager.EquipUserItem(itemName);
           

    //        //this sets the item in UI to be active depending on the status in the database
    //        equippedItems[index].SetActive(equippedStatus);
    //        ownedItems[index].GetComponentInChildren<TextMeshProUGUI>().text = equippedStatus ? "Equipped" : "";
    //    }
    //}


    //this is a helper function we can apply to all these dang lists ! 
    public void EquipItemHelper(Button[] ownedItem, GameObject[] equippedItem, int index)
    {
        string itemName = ownedItem[index].name.Replace("Display", "");
        inventoryManager.EquipUserItem(itemName);
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
        if (equippedHeadgear[Index].activeSelf)
        {
            headgear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedHeadgear[Index].SetActive(false);

        }
        else
        {
            for (int i = 0; i < headgear.Length; i++)
            {
                equippedHeadgear[i].SetActive(false);
                headgear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }


            GameObject itemToToggle = equippedHeadgear[Index];
            itemToToggle.SetActive(true);
            headgear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }
    }

    
    public void EquipEyewear(int Index)
    {
        if (equippedEyewear[Index].activeSelf)
        {
            eyewear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedEyewear[Index].SetActive(false);
        }
        else
        {
            for (int i = 0; i < eyewear.Length; i++)
        {
                equippedEyewear[i].SetActive(false);
                eyewear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
            GameObject itemToToggle = equippedEyewear[Index];
            itemToToggle.SetActive(true);
            eyewear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }


    public void EquipNeckwear(int Index)
    {
        if (equippedNeckwear[Index].activeSelf)
        {
            neckwear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedNeckwear[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < neckwear.Length; i++)
            {
                equippedNeckwear[i].SetActive(false);
                neckwear[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            GameObject itemToToggle = equippedNeckwear[Index];
            itemToToggle.SetActive(true);
            neckwear[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }


    public void EquipHandheld(int Index)
    {
        if (equippedHandheld[Index].activeSelf)
        {
            handheld[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedHandheld[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < handheld.Length; i++)
            {
                equippedHandheld[i].SetActive(false);
                handheld[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        GameObject itemToToggle = equippedHandheld[Index];
        itemToToggle.SetActive(true);
        handheld[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }


    public void EquipBody(int Index)
    {
        if (equippedBody[Index].activeSelf)
        {
            body[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedBody[Index].SetActive(false);

        }
        else
        {
            for (int i = 0; i < body.Length; i++)
            {
                equippedBody[i].SetActive(false);
                body[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        GameObject itemToToggle = equippedBody[Index];
        itemToToggle.SetActive(true);
        body[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }




    ////furniture equip by section

    public void EquipDecor(int Index)
    {
        EquipItemHelper(decor, equippedDecor, Index);

    }


    //if (equippedDecor[Index].activeSelf)
    //{
    //    decor[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //    equippedDecor[Index].SetActive(false);

    //}
    //else
    //{

    //    for (int i = 0; i < decor.Length; i++)
    //    {
    //        equippedDecor[i].SetActive(false);
    //        decor[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
    //    }
    //GameObject itemToToggle = equippedDecor[Index];
    //itemToToggle.SetActive(true);
    //decor[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
    //}



    public void EquipCouches(int Index)
    {
        if (equippedCouch[Index].activeSelf)
        {
            couches[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedCouch[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < couches.Length; i++)
            {
                equippedCouch[i].SetActive(false);
                couches[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        GameObject itemToToggle = equippedCouch[Index];
        itemToToggle.SetActive(true);
        couches[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }

    public void EquipTables(int Index)
    {
        if (equippedTable[Index].activeSelf)
        {
            tables[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedTable[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < tables.Length; i++)
            {
                equippedTable[i].SetActive(false);
                tables[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }


        GameObject itemToToggle = equippedTable[Index];
        itemToToggle.SetActive(true);
        tables[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }

    public void EquipRugs(int Index)
    {
        if (equippedRug[Index].activeSelf)
        {
            rugs[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedRug[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < rugs.Length; i++)
            {
                equippedRug[i].SetActive(false);
                rugs[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
   
        GameObject itemToToggle = equippedRug[Index];
        itemToToggle.SetActive(true);
        rugs[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }


    public void EquipWallpapers(int Index)
    {
        if (equippedWallpaper[Index].activeSelf)
        {
            wallpapers[Index].GetComponentInChildren<TextMeshProUGUI>().text = "";
            equippedWallpaper[Index].SetActive(false);

        }
        else
        {

            for (int i = 0; i < wallpapers.Length; i++)
            {
                equippedWallpaper[i].SetActive(false);
                wallpapers[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }



            GameObject itemToToggle = equippedWallpaper[Index];
            itemToToggle.SetActive(true);
            wallpapers[Index].GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }

    }


}




























