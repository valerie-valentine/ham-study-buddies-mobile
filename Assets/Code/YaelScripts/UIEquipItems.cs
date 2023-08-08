using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipItems : MonoBehaviour
{
    //game objects for head
    //objects for eyes
    //neck
    //hands
    // body

    public Button[] headgear;
    public Button[] eyewear;
    public Button[] neckwear;
    public Button[] handheld;
    public Button[] body;

    //object for couch
    //table
    //rug
    //decor

    public Button[] couches;
    public Button[] tables;
    public Button[] rugs;
    public Button[] decor;

    

    void Awake()
    {
       //instances here
       
    }

   // functions here that we attach to on click, toggle button on/off to equip and unequip
   //need to establish a parameter that takes area of room/ body?
   // when couch is equipped, no otehr furniture can be equipped with that room area
   //same for clothes.
}
