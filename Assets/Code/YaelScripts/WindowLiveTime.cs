using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLiveTime : MonoBehaviour
{

    public GameObject Night;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Get the current system time
        System.DateTime currentTime = System.DateTime.Now;

        // Define the start and end times for inactive hours (7 AM and 7 PM)
        System.DateTime startTime = new System.DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 0, 0);
        System.DateTime endTime = new System.DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 19, 0, 0);

        // Check if the current time is within the inactive hours
        if (currentTime >= startTime && currentTime <= endTime)
        {
            // Deactivate the night sky GameObject
            Night.SetActive(false);
        }
        else
        {
            // Activate the night sky GameObject
            Night.SetActive(true);
        }
    }
}
