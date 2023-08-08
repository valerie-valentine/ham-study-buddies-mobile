using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIBuyItems : MonoBehaviour
{
    //create variables and instances
    public static UIBuyItems instance;



    //cant use enabled or interactible on gameobject, has to be button specifically
    public Button [] shoppingFurniture;
    public GameObject[] ownedFurniture;

    public Button[] shoppingAccessories;
    public GameObject[] ownedAccessories;




    // Start is called before the first frame update
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

    public void BuyFurniture(int FurnitureIndex)
    {
        for (int i = 0; i < shoppingFurniture.Length; i++)
        {
            if (i == FurnitureIndex)
                ownedFurniture[i].SetActive(true);
            shoppingFurniture[FurnitureIndex].interactable = false;
            //change shopping furniture button tmp text to "owned"
            shoppingFurniture[FurnitureIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Owned";


        }
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

