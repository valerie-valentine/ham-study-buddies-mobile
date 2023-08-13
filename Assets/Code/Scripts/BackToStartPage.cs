using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToStartPage : MonoBehaviour
{
    [SerializeField] Button _backButton;

    void Start()
    {

        _backButton.onClick.AddListener(goToStartPage);
    }

    private void goToStartPage()
    {
        ScenesManager.Instance.LoadStartPage();
    }

}