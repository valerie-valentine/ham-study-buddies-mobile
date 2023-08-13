using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    public static NavigationBar instance;

    public GameObject[] navButtons;
    public GameObject[] navWindows;


    void Awake()
    {
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

    public void ShowActiveWindow(int windowIndex)
    {

        for (int i = 0; i < navButtons.Length; i++)
        {
            if (i != windowIndex)
                navWindows[i].SetActive(false);
        }

        GameObject windowToToggle = navWindows[windowIndex];

        windowToToggle.SetActive(!windowToToggle.activeSelf);
    }

}

