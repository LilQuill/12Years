using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    //music player
    private GameObject musicplayer;
    private MusicController music;

	float timeStart;
	public float length = 30f; //amount of seconds for level
	public float startHour = 3f; //time started
	public float endHour = 12f; //MUST BE IN PM (1:00 is 13:00)

	float hourLength;

	private bool active;
	[HideInInspector]
	public int stressTick = 5;
	public int bedtimeStressTick;
	int minutes;
    float minutesTemp; //helps code minutes better so that i don't like die
	float time;
	bool paused;
	bool instructions;
	float trashSpeed;
	float laundrySpeed;
	bool addedStress;
	bool bedtime = false;



	GameObject pauseScreen;
	GameObject instructionsScreen;
	GameObject begin;
	GameObject mainMenu;
	GameController controller;



	Animator trash;
	Animator laundry;
	Animator student;
	Animator door;

	// Use this for initialization
	void Start () {
		
		addedStress = false;
		active = false;
		paused = false;
		instructions = false;
		hourLength = length / (endHour - startHour);
		timer ();
		trash = GameObject.Find("Trash Can").GetComponent<Animator>();
		laundry = GameObject.Find("Laundry").GetComponent<Animator>();
		student = GameObject.Find ("Student").GetComponent<Animator> ();
		door = GameObject.Find ("Door").GetComponent<Animator> ();
		begin = GameObject.FindObjectsOfType<InstructionsScreenDONTDELETE>()[0].gameObject;
		mainMenu = GameObject.Find ("MainMenu");
		pauseScreen = transform.GetChild(0).gameObject;
		instructionsScreen = transform.GetChild(1).gameObject;
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();


        musicplayer = GameObject.Find("Music Player");
        music = musicplayer.GetComponent<MusicController>();
		int day = GameController.day;
		if (day == 1) {
			mainMenuToggle ();
			pause ();
			music.playMenuMusic ();
		} else {
			music.playMusic ();
		}
		EndLevel.playedDefeat = false;

        
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused && active) {
			time += Time.deltaTime;
		}  
		Debug.Log (time);

		if (active && time >= timeStart + length) {
			timerEnd ();
			time = 0;
		}
		//Debug.Log (paused);
		//tester
		if (Input.GetKeyDown(KeyCode.Escape)) {
			
			if (!instructions) {
				pauseMenuToggle ();
				pause ();
			} else {
				instructionsToggle ();
			}
			
		}

		//adds Stress Tick
		if (!addedStress && time % stressTick < 0.4) {
			Debug.Log ("AAAaAAAA");
			BackgroundStats.changeStress (1);
			Debug.Log (bedtimeStressTick);
			Debug.Log ("stress tick: " + stressTick);
			addedStress = true;
			if(bedtime) {
				time = 0;
			}
		} 
		if (time % stressTick > 0.5) {
			
			addedStress = false;
		}
        
	}


	//timer initializes
	void timer() { 
		time = 0;
		active = true;
	}


	//called when timer is up
	void timerEnd() {
		controller.Bedtime ();
	}

    //public functions
	public float GetTimeElapsed() {
		return time;
	}

	public int GetHour() {
		return (int)(startHour + (GetTimeElapsed()/hourLength));
	}

	public float GetTimeRemaining() {
		return length - (GetTimeElapsed());
	}

    public float getHourLength() {
        return hourLength;
    }

	public void startBedtime() { //bedtime will set the time to zero and reset each time the time reaches the bedtimeStressTick
		stressTick = bedtimeStressTick;
		bedtime = true;
		time = 0;
	}
	
	public int getSleepHours() { //returns amount of sleep
		if (bedtime)
			return 6;
		else
			return (int)(endHour - GetHour () + 7);
	}

    
	//Returns the string that should be used in the TimeDisplay class
	public string getTimeDisplay()
    {
		string td;
		if (bedtime) {
			td = "BEDTIME";
			return td;
		}
		minutesTemp = ((int)GetTimeElapsed() % hourLength) * (60 / hourLength);
        minutes = (int)minutesTemp;
        //The minutes value on this string increments every second
        //by 60 / (length of an hour from the field above)
        if (GetHour() < 10) //makes sure that if hours is a single digit, it has a 0 before it so it doesn't shift
        {
            td = "0" + GetHour() + ":";
        }
        else
        {
            td = GetHour() + ":";
        }

        if (minutes < 10)
        {
            td = td + "0" + minutes; //makes sure minutes is always 2 digits
        }
        else
        {
            td = td + minutes;
        }
        //debug this sakshi you fool
		return td;
    }
    

	public void pause() {
		door.enabled = !door.enabled;
		student.enabled = !student.enabled;
		trash.enabled = !trash.enabled;
		laundry.enabled = !laundry.enabled;
		music.toggleQuietMusic();
		paused = !paused;
		GameObject[] clickables = GameObject.FindGameObjectsWithTag ("Clickable");
		for (int i = 0; i < clickables.Length; i++) {
			//tries to disable boxcolliders and if fails tries polygons and if fails does nothing
			try {
				
				clickables [i].GetComponent<BoxCollider2D> ().enabled = !clickables [i].GetComponent<BoxCollider2D> ().enabled;
			} catch {
				try {
					clickables [i].GetComponent<PolygonCollider2D> ().enabled = !clickables [i].GetComponent<PolygonCollider2D> ().enabled;
				} catch {
				}
			}
		}
	}

	public void pauseMenuToggle() { //IT WORKS AAAHHAHA
		pauseScreen.SetActive (!pauseScreen.activeInHierarchy);
		instructionsScreen.SetActive (false);
        
	}

	public bool isPaused() {
		return paused;
	}

	public void  beginningInstructionsToggle() {
		
		pauseScreen.SetActive (false);
		foreach (Transform child in begin.transform) {
			
			child.gameObject.SetActive (!child.gameObject.activeInHierarchy);
		}
//		begin.transform.getChild (!begin.activeInHierarchy);
		instructions = !instructions;
	}

	public void mainMenuToggle() {
		
		instructionsScreen.SetActive (false);
		//instructions = false;

		foreach (Transform child in mainMenu.transform) {
			
			child.gameObject.SetActive (!child.gameObject.activeInHierarchy);
		}
	}

	public void  instructionsToggle() {
		
		pauseScreen.SetActive (false);
		instructionsScreen.SetActive (!instructionsScreen.activeInHierarchy);
		instructions = !instructions;

	}

    public void quitGame()
    {

        Debug.Log("Application is quitting");
        Application.Quit();
    }





}
