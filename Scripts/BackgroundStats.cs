using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BackgroundStats  {
    //MAKE EVERY FIELD AND METHOD IN HERE STATIC PLEASE!

	//length for 2 stress
	static float stressLength;//movement for 1 stress
	static float stressBarLength = 4.6f;//whole length
	static float currentPos;

    //stats
    static IndividualStats energy = new IndividualStats();
    static IndividualStats friend = new IndividualStats();
    static IndividualStats family = new IndividualStats();
    static IndividualStats bae = new IndividualStats();
    static IndividualStats grades = new IndividualStats();
    public static IndividualStats stress = new IndividualStats(25);
	static Transform cursor;

    //timer reference
    static TimeManager timer;
    static GameObject clock;

    //Array of stats that makes coding way easier!
    //If you want to add more stats, make sure to add a field and also
    //call the constructor in Start () below
    static IndividualStats[] stats = { energy, friend, family, bae, grades, stress };

	// Use this for initialization
	public static void Start () { //Call this method in the Start () method of the GameManager or GameController or whatever
        clock = GameObject.Find("TimeManager");
        timer = (TimeManager)clock.GetComponent(typeof(TimeManager));
		stressLength = stressBarLength / 100;
		cursor = GameObject.Find ("StressMeter").transform.GetChild (0);

    }

    // Update is called once per frame
    public static void Update () { //Call this method in the Update () method of the GameManager or GameController or whatever
        //updateStats();

		updateStressBar ();
        checkStress();
    }

	//updating sprite
	public static void updateStressBar() {
		Vector3 temp = new Vector3((-(stressBarLength / 2) + stress.getStat () * stressLength), cursor.position.y, cursor.position.z);

		cursor.position = temp;
	}

    //check the stress for loss
    public static void checkStress()
    {
        if (stress.getStat() >= 100)
        {
            EndLevel.end(false, "stress");
        }
    }

    //These following "change_____" methods are p self-explanatory I think.
    //The change parameter should be negative if it's a reduction and positive if it's an addition
    public static void changeEnergy (int change)
    {
        energy.changeStat(change);
        Debug.Log("changed energy by" + change);
    }

    public static void changeFriend (int change)
    {
        friend.changeStat(change);
        Debug.Log("changed friend by" + change);
    }

    public static void changeFamily (int change)
    {
        family.changeStat(change);
        Debug.Log("changed family by" + change);
    }

    public static void changeBae (int change)
    {
        bae.changeStat(change);
        Debug.Log("changed bae by" + change);
    }

    public static void changeGrades (int change)
    {
        grades.changeStat(change);
        Debug.Log("changed grades by" + change);
    }

    public static void changeStress (int change)
    {
        stress.changeStat(change);
		if (stress.getStat () < 0) {
			stress.sakshiIsAFool (0);
		}
        stress.sakshiCantRead();
		Debug.Log (stress.getStat ());
        Debug.Log("changed stress by" + change);
    }

	public static int getStress() {
		return stress.getStat ();
	}


    //Makes sure none of the stats go over 100 or under 0
    public static void checkStatLimits ()
    {
        for (int x = 0; x < stats.Length; x++)
        {
            if ((stats[x].getStat() + stats[x].getChange()) > 100) //makes sure it doesn't go over 100
            {
                stats[x].changeStat(100 - stats[x].getStat());
            }
            else if ((stats[x].getStat() + stats[x].getChange()) < 0) //makes sure it doesn't go under 0
            {
                stats[x].changeStat((stats[x].getStat() + stats[x].getChange()) * -1);
                //PLEASE check my math here. I'm tired.
            }
            Debug.Log(stats[x].getStat() + ", " + stats[x].getChange());
        }
    }

    //Updates stats at the end of the day
    public static void updateStats ()
    {
        if ((timer.GetTimeElapsed() != 0 && (timer.GetTimeElapsed() % timer.length) == 0))
        {
            checkStatLimits(); //see method above
            //Show the end level screen here
            for (int x = 0; x < stats.Length; x++)
            {
                stats[x].setStat();
            }
        }
        Debug.Log("reset stat changes");
    }

    //Calculates stress stat based on others
    public static void calculateStress()
    {

    }

	public static void Reset() {
		stress.sakshiIsAFool (25);
	}

    //ends game at 100 stress
    public static void stressEnd()
    {
        if (stress.getStat() >= 100) //it shouldn't ever be greater than 100 but
        {
            EndLevel.end(false, "stress");
			stress.sakshiIsAFool (0);
        }
    }

}
