using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HWCounter : MonoBehaviour {

    Text txt;
	HomeworkInterface hwInterface;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
		hwInterface = GameObject.Find ("HomeworkInterface").GetComponent<HomeworkInterface> ();
		updateText ();
		toggleHWCounter (true);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void updateText() {
		Debug.Log (txt);
		Debug.Log (hwInterface);
		Debug.Log (hwInterface.HWLeft);
		txt.text = "(" + hwInterface.HWLeft.ToString() + ")";
	}

    public void toggleHWCounter()//turns HWcounter on and off during instruction menu
    {
        GetComponent<Text>().enabled = !GetComponent<Text>().enabled; 

    }

	public void toggleHWCounter(bool x)//turns HWcounter on and off during instruction menu
	{
		GetComponent<Text>().enabled = x; 

	}
}
