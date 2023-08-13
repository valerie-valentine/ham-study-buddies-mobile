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
        ShopDisplay();
        OwnedItemsDisplay();
        
        
    }


    public async void ShopDisplay()
    {
        List<string> inventoryItems = await inventoryManager.GetInventory(); 

        // Update the furniture items in the shop
        for (int furnitureIndex = 0; furnitureIndex < shoppingFurniture.Length; furnitureIndex++)
        {
            string furnitureName = shoppingFurniture[furnitureIndex].name.Replace("Buy", ""); // Extract the furniture name from the button name

            if (inventoryItems.Contains(furnitureName))
            {
                shoppingFurniture[furnitureIndex].interactable = false;
                shoppingFurniture[furnitureIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            }
        }

        // Update the accessories items in the shop
        for (int accessoriesIndex = 0; accessoriesIndex < shoppingAccessories.Length; accessoriesIndex++)
        {
            string accessoriesName = shoppingAccessories[accessoriesIndex].name.Replace("Buy", ""); // Extract the accessories name from the button name

            if (inventoryItems.Contains(accessoriesName))
            {
                shoppingAccessories[accessoriesIndex].interactable = false;
                shoppingAccessories[accessoriesIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            }
        }
    }

    public async void OwnedItemsDisplay()
    {
        List<string> inventoryItems = await inventoryManager.GetInventory();

        for (int furnitureIndex = 0; furnitureIndex < shoppingFurniture.Length; furnitureIndex++)
        {
            string ownedFurnitureName = ownedFurniture[furnitureIndex].name;

            if (inventoryItems.Contains(ownedFurnitureName))
            {
                ownedFurniture[furnitureIndex].SetActive(true);
            }
        }

        for (int accessoriesIndex = 0; accessoriesIndex < shoppingFurniture.Length; accessoriesIndex++)
        {
            string ownedAccessoriesName = ownedAccessories[accessoriesIndex].name;

            if (inventoryItems.Contains(ownedAccessoriesName))
            {
                ownedAccessories[accessoriesIndex].SetActive(true);
            }
        }
    }




    public async void BuyFurniture(string FurnitureData)
    {
        var currencyManager = CurrencyManager.instance;
        string delimiter = "_";
        List<string> stringList = new List<string>();
        string[] parts = FurnitureData.Split(delimiter);
        stringList.AddRange(parts);

        string name = stringList[0];
        string type = stringList[1];
        string subtype = stringList[2];   //headgear, add the rest here 
        int price = int.Parse(stringList[3]);
        int FurnitureIndex = int.Parse(stringList[4]);
        float? userCurrency = await currencyManager.GetCurrency();


        if (price < userCurrency) {
            for (int i = 0; i < shoppingFurniture.Length; i++)
            {
                if (i == FurnitureIndex)
                    ownedFurniture[i].SetActive(true);
                shoppingFurniture[FurnitureIndex].interactable = false;
                shoppingFurniture[FurnitureIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            }

            inventoryManager.BuyItemDatabase(name, type, subtype, price);
        }
        
    }
    // exactly the same as buy furniture but separation of concerns

    public async void BuyAccessories(string AccessoriesData)
        {
        var currencyManager = CurrencyManager.instance;
        string delimiter = "_";
        List<string> stringList = new List<string>();
        string[] parts = AccessoriesData.Split(delimiter);
        stringList.AddRange(parts);

        string name = stringList[0];
        string type = stringList[1];
        string subtype = stringList[2];
        int price = int.Parse(stringList[3]);
        int AccessoriesIndex = int.Parse(stringList[4]);
        float? userCurrencyAgain = await currencyManager.GetCurrency();

        if (price < userCurrencyAgain)
        {
            for (int i = 0; i < shoppingAccessories.Length; i++)
            {
                if (i == AccessoriesIndex)
                    ownedAccessories[i].SetActive(true);
                shoppingAccessories[AccessoriesIndex].interactable = false;
                shoppingAccessories[AccessoriesIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";
            }
            inventoryManager.BuyItemDatabase(name, type, subtype, price);
        }
    }

  }
