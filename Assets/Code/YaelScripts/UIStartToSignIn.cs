using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartToSignIn : MonoBehaviour
{
    [SerializeField] Button _signIn;

    void Start()
    {

        _signIn.onClick.AddListener(goToSignIn);
    }

    private void goToSignIn()
    {
        ScenesManager.Instance.LoadSignInPage();
    }

}