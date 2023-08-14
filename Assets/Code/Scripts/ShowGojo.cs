using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGojo : MonoBehaviour
{
    
    public GameObject objectToShow;

    public void ShowAndHideObject()
    {
        StartCoroutine(ShowObjectCoroutine());
    }

    private IEnumerator ShowObjectCoroutine()
    {
        objectToShow.SetActive(true);

        yield return new WaitForSeconds(1.0f); // Show the object for 1 second

        objectToShow.SetActive(false);
    }


}
