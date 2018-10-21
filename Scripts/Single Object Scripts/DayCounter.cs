using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour {

	GameController controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();
		Text txt = GetComponent<Text> ();
		txt.text = "Day " + controller.getDay ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
