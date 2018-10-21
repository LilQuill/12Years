using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {

    //Checking time stuff
    private int startTime;
    private int intervalMax;
	public int intervalMin = 1;
    private int entryTime;
	private int knockTime;
	private int soundTime;
    private bool timeIsSet;
    public int knockEnds = 3; //This is based on the knocking animation off the door
    //Set knockEnds to whatever makes sense based on the animation length in Start ()
    public float doorCloseTime = 3; //See above comment
	public float momMinVisits = 1;

    //For the reaction
    private bool isAngry;
	private bool gameEnded;
	private bool soundPlayed;
	private bool penaltyApplied; //checking if the penalty has been applied already
	private bool momIsHere;
	private bool knocking;
    private int penalty = 25; 
    private int plus = 10; //this should be positive. It's the addition in the family stat if she's happy

    //References
    private TimeManager timer;
    private GameObject clock;
    private TrashController trashcan;
    private GameObject can;
    private LaundryController laundry;
    private GameObject laundrybin;
	private BoxCollider2D doorCollider;
	private Animator momAnim;
	private Animator anim;
	private AudioSource momAudio;
    [SerializeField] private AudioClip angryClip;
    [SerializeField] private AudioClip happyClip;
    [SerializeField] private AudioClip knockClip;
	[SerializeField] private AudioClip playerKnockClip;


    // Use this for initialization
    void Start () {
        isAngry = false;
        timeIsSet = false;

		clock = GameObject.FindGameObjectWithTag("Timer");
		timer = clock.GetComponent<TimeManager>();
        can = GameObject.Find("Trash Can");
		trashcan = can.GetComponent<TrashController>();
        laundrybin = GameObject.Find("Laundry");
        laundry = laundrybin.GetComponent<LaundryController>();
        momAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
		doorCollider = gameObject.GetComponent<BoxCollider2D> ();
		anim = GetComponent<Animator>();
		momAudio = GetComponent<AudioSource> ();

		intervalMax = (int) (timer.length/momMinVisits-1); //This is based on the level length in TimeManager

		knocking = false;
		penaltyApplied = false;
		momIsHere = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!timeIsSet) {
			setInterval (); 
		}
		enter();
        //changeFamilyStat();
        momLeaves();
	}

	//Decides time to enter randomly
    void setInterval()
    {
			startTime = (int) (timer.GetTimeElapsed());
			setInterval(Random.Range(startTime+intervalMin, startTime+intervalMax));
    }

	//for changing the time (answering the door)
	void setInterval(int knockTimeChosen) {
		anim.SetBool ("closing", false);

		knockTime = knockTimeChosen;
		entryTime = (int) (knockTime + knockEnds);
		soundTime = entryTime +1;
		timeIsSet = true;
		Debug.Log ("Knocking: " + knockTime + "  Will enter: "+entryTime);
	}

    //Checks time to see if should enter
    void enter()
    {
		if ((int)(timer.GetTimeElapsed ()) == knockTime && !knocking) {
			momKnock();
		}

		if ((int)(timer.GetTimeElapsed()) == entryTime && !penaltyApplied)
        {
			momEnter();
        }

		if ((int)(timer.GetTimeElapsed ()) == soundTime && !soundPlayed) {
			momAudio.time = 0f;
			momAudio.Play ();
			soundPlayed = true;
		}
    }

	void momKnock() { //starts knocking animations
		anim.SetBool ("knocking", true);
		knocking = true;
		momAudio.time = 0f;
		momAudio.clip = knockClip;
		momAudio.volume = .5f;
		momAudio.Play ();
	}

	void momEnter() { //checks everything and applies penalties
		knocking = false;
		momIsHere = true;
		Debug.Log ("entering");
		if (trashcan.IsTrashFull())
		{
			isAngry = true;
			changeStressStat();
			penaltyApplied = true;
			
			gameEnded = true;
			momAnim.SetBool("momAngry", true);
			momAudio.clip = angryClip;
			momAudio.volume = 1;
			
			
			//Put the angry face animation here
			//And speech bubbles and stuff if you want
		}
		else if (laundry.IsLaundryFull())
		{
			isAngry = true;
			
			
			gameEnded = true;
			momAnim.SetBool("momAngry", true);
			momAudio.clip = angryClip;
			momAudio.volume = 1;
		}
		else
		{
			isAngry = false;
			penaltyApplied = true;
			momAnim.SetBool("momAngry", false);
			momAudio.clip = happyClip;
			momAudio.volume = 1;
			//BackgroundStats.changeFamily(plus);
			
			//Put the happy face animation here
			//And speech bubbles and stuff if you want
		}
		anim.SetBool ("mom", true);
		anim.SetBool ("knocking", false);

	}

    //changes the family stat based on isAngry and penaltyApplied
    void changeStressStat()
    {
        if (!penaltyApplied)
        {
            if (isAngry)
            {
                BackgroundStats.changeStress(penalty);
                Debug.Log("subtracted from family stat");
            }
            else
            {
                BackgroundStats.changeFamily(plus);
                Debug.Log("added to family stat");
            }
        }
    }

    //makes everything normal again
    void momLeaves()
    {
		
		if ((int)(timer.GetTimeElapsed()) == (entryTime + doorCloseTime))
        {
			anim.SetBool ("closing", true);
			anim.SetBool ("mom", false);
			Debug.Log ("Leaving: " + (int)(timer.GetTimeElapsed()));
			changeStressStat();
			penaltyApplied = true;
//			if (gameEnded) {
//				//EndLevel.end (false,"mother");
//			}
			timeIsSet = false;
			penaltyApplied = false;
			soundPlayed = false;
			momIsHere = false;
        }

    }

    //Returns mother's mood. Ends game for rough playable version on Tuesday
    public bool momMood()
    {
        return isAngry;
    }

	private void OnMouseOver()
	{
		if (knocking) {
			anim.SetLayerWeight (1, 1);
		} else {
			anim.SetLayerWeight (1, 0);
		}
	}

	private void OnMouseExit()
	{
		anim.SetLayerWeight(1, 0);
	}

	private void OnMouseDown() {
		if (knocking) {
			setInterval ((int)(timer.GetTimeElapsed () - knockEnds));

			momEnter ();
		} else if (!momIsHere) {
			momAudio.clip = playerKnockClip;
			momAudio.volume = 1;
			momAudio.time = 0.878f;
			momAudio.Play();
		}

	}

}



