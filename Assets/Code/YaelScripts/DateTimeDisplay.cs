using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateTimeDisplay : MonoBehaviour
{

    public GameObject _timeDisplay;
    public System.DateTime _date;
    public int _hour;
    public int _minutes;



    // Start is called before the first frame update
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        _date = System.DateTime.Now;
        _timeDisplay.GetComponent<Text>().text = "" + _date;
    }
}