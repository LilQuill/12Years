using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	
    public static GameController instance;
    public GameObject endScreen;
    public bool gameOver = false;

	//stats that affect things, each array contains the numbers for each day
	public static int day = 1;
	float[] trashFillTime = { 18f, 16f, 14f, 12f, 10f };
	float[] trashEmptyTime = { 2f, 2f, 2f, 1f, 1f };
	float[] laundryFillTime = { 24f, 22f, 20f, 18f, 17f };
	float[] laundryEmptyTime = { 4f, 4f, 3f, 3f, 2f };
	int[] homeworkLoad = { 8, 11, 14, 16, 18 };
	int[] momMin = { 12, 10, 6, 4, 1 };
	int[] friendPatience = { 2, 2, 1, 1, 1 };
	float[] momMaxMod = { 1f, 2f, 3f, 4f, 7f }; //minimum visits per day
	int defaultStressTickRate = 7; //seconds between each passive stress tick
	int bedtimeStressTickRate = 1;
	float dayLength = 10f;

	//may need to make separate prefabs for all of these
	int[] minWords = { 1, 2, 3, 3, 4 };
	int[] maxWords = { 3, 5, 6, 7, 8 };
	int[] maxNumber = { 4, 7, 10, 11, 13 };

	//trash, laundry, mother/door class, 
	TrashController trash;
	LaundryController laundry;
	Mother mother;
	HomeworkInterface hw;
	WindowController window;
	BedController bed;
	BedtimeFilterController bedtimeFilter;
	TimeManager timeManager;

	//accessed by hw prefabs
	[HideInInspector]
	public int maxNumberAccess;
	public int maxWordsAccess;
	public int minWordsAccess;

    // OCCURS BEFORE INITIALIZATION
    void Awake()
    {


		// CHECKS IF THERE ARE OTHER INSTANCES OF GAMECONTROLLER
        if (instance == null)
        {
            instance = this;
        }
        //DESTROYS ITSELF IF ANOTHER INSTANCE EXISTS
        else if (instance != null)
        {
            Destroy(this);
        }

		trash = GameObject.Find("Trash Can").GetComponent<TrashController>();
		laundry = GameObject.Find("Laundry").GetComponent<LaundryController>();
		mother = GameObject.Find("Door").GetComponent<Mother>();
		hw = GameObject.Find ("HomeworkInterface").GetComponent<HomeworkInterface> ();
		window = GameObject.Find ("Window").GetComponent<WindowController> ();
		timeManager = GameObject.Find ("TimeManager").GetComponent<TimeManager> ();
		bed = GameObject.Find ("Bed").GetComponent<BedController> ();
		bedtimeFilter = GameObject.Find ("Bedtime Filter").GetComponent<BedtimeFilterController> ();
		setDayStats ();
    }

	void Start() {
		BackgroundStats.Start ();
		timeManager.stressTick = defaultStressTickRate;
		timeManager.bedtimeStressTick = bedtimeStressTickRate;
	}

    // Update is called once per frame
    void Update()
    {
		BackgroundStats.Update ();
	}

    public void Restart()   //RESTARTS THE GAME
    {
		BackgroundStats.stress.sakshiIsAFool (60);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

	public void NextDay()   //NEXT DAY
	{
		day++;
		Debug.Log ("Day: " + day); 
		setDayStats ();
		//ADD IN STRESS DECREASE FROM SLEEP HERE
		if (day > 5) {
			EndLevel.win ();
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	public void Bedtime() {
		bed.setActive (true);
		//visual nighttime effect
		timeManager.startBedtime ();
		bedtimeFilter.setBedtime (true);
	}

	public void ResetGame() {
		BackgroundStats.Reset ();
		Debug.Log ("reset");
		day = 1;
		BackgroundStats.stress.sakshiIsAFool (40);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void setDayStats() 
	{
		trash.timeToFull = trashFillTime [day - 1];
		trash.emptySpeed = trashEmptyTime [day - 1];
		laundry.timeToFull = laundryFillTime [day - 1];
		laundry.emptySpeed = laundryEmptyTime [day - 1];
		maxNumberAccess = maxNumber [day - 1];
		maxWordsAccess = maxWords [day - 1];
		minWordsAccess = minWords [day - 1];
		window.patience = friendPatience [day - 1];

		timeManager.bedtimeStressTick = bedtimeStressTickRate;
		timeManager.length = dayLength;

		hw.HWAmount = homeworkLoad [day - 1];
		hw.HWAmntSet = true;
		mother.intervalMin = momMin [day - 1];
		mother.momMinVisits = momMaxMod [day - 1];
		Debug.Log ("day: "+day+"\n trash length: " + trash.timeToFull + "\n trash empty speed: " + trash.emptySpeed +
			"\n laundry length: " + laundry.timeToFull + "\n laundry empty speed: " + laundry.emptySpeed +
			"\n mom min interval: " + mother.intervalMin + "\n mom min visits: " + mother.momMinVisits);
	}

	public int getDay() {
		return day;
	}


}