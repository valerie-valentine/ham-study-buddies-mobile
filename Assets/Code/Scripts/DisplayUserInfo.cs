using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DisplayUserInfo : MonoBehaviour
{
    public UserManager userManager;
    public showUsersHamster showUsersHamster;

    public TextMeshProUGUI userNameText;


    private void Awake()
    {
       ShowUserInfo();
    }


public void ShowUserInfo()
{
    userManager = UserManager.instance;
    //showUsersHamster = showUsersHamster.instance;


    userNameText.text = userManager.User.DisplayName;
        //string hamsterText = await showUsersHamster.instance.GetHamster();
        //finalHamster.text = hamsterText;
    
}

}