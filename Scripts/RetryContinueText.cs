using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryContinueText : MonoBehaviour {

    private Text txt;
	GameController controller;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        txt.enabled = false;
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
    public void retryMode()
    {
        txt.enabled = true;
        txt.text = "Retry";
		transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
		transform.parent.GetComponent<Button>().onClick.AddListener(delegate { controller.ResetGame(); });
    }

    public void continueMode()
    {
        txt.enabled = true;
        txt.text = "Continue";
		transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();

		transform.parent.GetComponent<Button>().onClick.AddListener(delegate { controller.NextDay(); });

    }

	public void winMode()
	{
		txt.enabled = true;
		txt.text = "Start Over";
		transform.parent.GetComponent<Button> ().onClick.RemoveAllListeners ();
		transform.parent.GetComponent<Button>().onClick.AddListener(delegate { controller.ResetGame(); });
	}
}
