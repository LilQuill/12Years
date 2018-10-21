using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class is supposed to just enable user input,

public class Typable : MonoBehaviour {

	Text  gt;
	string output;
	public GameObject copiedText;
	bool submissionComplete;
    HomeworkManager manager;
    HomeworkInterface OwO;

    //GradeController reference
    GameObject graCon;
    GradeController gc;

    
    //int grade; //this holds the percent grade

	void Start()
	{
		gt = GetComponent<Text>();
		gt.text = "";
		manager = transform.parent.parent.parent.gameObject.GetComponent<HomeworkManager>();
		Debug.Log (manager);
		OwO = manager.transform.parent.gameObject.GetComponent<HomeworkInterface>();
	}
	
	void Update()
	{
        //if this breaks bc it's in a method, it was literally just
        //the willIsAFool() method
        willIsAFool();
	}

    void willIsAFool()
    {
		foreach (char c in Input.inputString) {
			if (c == '\b') { // has backspace/delete been pressed?
				if (gt.text.Length != 0) {
					gt.text = gt.text.Substring (0, gt.text.Length - 1);
				}
			} else if ((c == '\n') || (c == '\r')) { // enter/return
				if (!submissionComplete) {
					OwO.submitted = true;
					output = gt.text;
					manager.checkDone (output);
					submissionComplete = true;
				}
					

			} else {
				gt.text += c;

			}
		}
	}

	public void setSubmissionComplete(bool x){ //for preventing double submits
		submissionComplete = x;
	}
	//PLEASE DONT ADD MORE THINGS HERE FOR HW CHECKING
}
