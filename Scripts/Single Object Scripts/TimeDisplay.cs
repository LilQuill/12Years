using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{

    private GameObject clock;
    private TimeManager timer;
    private Text txt;

    void Start()
    {
        clock = GameObject.Find("TimeManager");
        timer = (TimeManager)clock.GetComponent(typeof(TimeManager));
        txt = GetComponent<Text>();
        txt.text = "0" + timer.startHour + ":00";
    }

    void Update()
    {
        setDisplay();
    }

    void setDisplay()
    {
        if (timer.GetHour() <= timer.endHour)
        {
            txt.text = timer.getTimeDisplay();
            //Debug.Log("printed" + timer.getTimeDisplay());
        }
        else
        {
            txt.text = "BEDTIME";
        }
        //Debug.Log("printed " + timer.getTimeDisplay());
    }
}
