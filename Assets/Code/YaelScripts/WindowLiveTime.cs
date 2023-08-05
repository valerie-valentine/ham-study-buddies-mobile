using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLiveTime : MonoBehaviour
{
    public GameObject Night;   // 8pm to 7am
    public GameObject Sunrise; // 7am to 8am
    public GameObject Rainbow; // 3pm to 5pm
    public GameObject Sunset;  // 7pm to 8pm

    // Start is called before the first frame update
    void Start()
    {
        UpdateSkyObjects(); // Call the method once at start to set the initial state
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSkyObjects(); // Call the method in Update to update the state every frame
    }

    void UpdateSkyObjects()
    {
        System.DateTime currentTime = System.DateTime.Now;

        // Check the current time and activate/deactivate the corresponding game objects
        Night.SetActive(currentTime.Hour >= 20 || currentTime.Hour < 7);
        Sunrise.SetActive(currentTime.Hour >= 7 && currentTime.Hour < 8);
        Rainbow.SetActive(currentTime.Hour >= 15 && currentTime.Hour < 17);
        Sunset.SetActive(currentTime.Hour >= 19 && currentTime.Hour < 20);
    }
}
