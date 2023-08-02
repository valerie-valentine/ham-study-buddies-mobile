using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartToSignUp : MonoBehaviour
{
    [SerializeField] Button _signUp;

    void Start()
    {

        _signUp.onClick.AddListener(goToSignUp);
    }

    private void goToSignUp()
    {
        ScenesManager.Instance.LoadSignUpPage();
    }

}