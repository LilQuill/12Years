using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseReason : MonoBehaviour {

    private Text txt;
    private GameObject clock;
    private TimeManager timer;

	void Start ()
    {
        txt = GetComponent<Text>();
        txt.enabled = false;
        clock = GameObject.Find("TimeManager");
        timer = clock.GetComponent<TimeManager>();
	}
	
    //justification for mother loss
    public void displayMotherLoss()
    {
        txt.enabled = true;
        txt.text = "Your mother got mad at your dirty room.";
    }

    //justification for bedtime loss
    public void displayBedtimeLoss()
    {
        txt.enabled = true;
        txt.text = "You didn't finish your homework before bedtime.";
    }

    //justification for stress loss
    public void displayStressLoss()
    {
        txt.enabled = true;
        txt.text = "You got too stressed.";
    }

    //show how many hours are left
    public void displaySleepHours()
    {
        txt.enabled = true;
		int sleepHours = timer.getSleepHours ();
		txt.text = "You got " + sleepHours + " hours of sleep.";
		BackgroundStats.changeStress (Mathf.Max(-(sleepHours - 6) * 6, -30));
    }

    //closes the text
    public void closeJustification()
    {
        txt.enabled = false;
    }

    //this should never be thrown unless EndLevel.end() is called incorrectly
    public void errorMethod()
    {
        txt.enabled = true;
        txt.text = "This should not happen.";
    }
}