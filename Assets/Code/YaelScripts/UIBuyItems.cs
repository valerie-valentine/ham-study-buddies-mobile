using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIBuyItems : MonoBehaviour
{
    //create variables and instances
    public InventoryManager inventoryManager;



    //cant use enabled or interactible on gameobject, has to be button specifically
    public Button [] shoppingFurniture;
    public GameObject[] ownedFurniture;

    public Button[] shoppingAccessories;
    public GameObject[] ownedAccessories;




    // Start is called before the first frame update
    void Awake()
    {
  
        inventoryManager = InventoryManager.instance;
        
        
    }

    //public void BuyFurniture(int FurnitureIndex)
    //{
    //    for (int i = 0; i < shoppingFurniture.Length; i++)
    //    {
    //        if (i == FurnitureIndex)
    //            ownedFurniture[i].SetActive(true);
    //        //owneFurniture is what is in users database
    //        shoppingFurniture[FurnitureIndex].interactable = false;
    //        shoppingFurniture[FurnitureIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";



    //    }
    //}

    public void BuyFurniture(string FurnitureData)
    {
        string delimiter = "_";
        List<string> stringList = new List<string>();
        string[] parts = FurnitureData.Split(delimiter);
        stringList.AddRange(parts);

        string name = stringList[0];
        string type = stringList[1];
        int price = int.Parse(stringList[2]);
        int FurnitureIndex = int.Parse(stringList[3]);

        for (int i = 0; i < shoppingFurniture.Length; i++)
        {
            if (i == FurnitureIndex)
                ownedFurniture[i].SetActive(true);
            //owneFurniture is what is in users database
            shoppingFurniture[FurnitureIndex].interactable = false;
            shoppingFurniture[FurnitureIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
        }

        inventoryManager.BuyItemDatabase(name, type, price);
    }
    // exactly the same as buy furniture but separation of concerns

    public void BuyAccessories(int ItemIndex)
        {
            for (int i = 0; i < shoppingAccessories.Length; i++)
            {
                if (i == ItemIndex)
                    ownedAccessories[i].SetActive(true);
                shoppingAccessories[ItemIndex].interactable = false;
                
                shoppingAccessories[ItemIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";


            }

        }

  }

