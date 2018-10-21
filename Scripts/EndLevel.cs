using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EndLevel { //IGNORE LITERALLY EVERYTHING EXCEPT FOR END()

    //fields
    static bool gameOver;
    static bool isVictory;

    //references
    static TimeManager timer;
    //static GameObject clock;
    
    static GameObject screen;
    static EndScreenController endscreen;
    static GameObject losejustification;
    static LoseReason reason;

    //For other things to be able to end the game. This takes a boolean parameter so don't forget that!!
    //!x is defeat, x is victory
    static public void end(bool x)
    {
        loadReferences();
		timer.pause ();
        if (x)
        {
 /*           if (GameController.day == 5)
            {
                Debug.Log("You win!");
                endscreen.winscreen();
                reason.displaySleepHours();
            } else
            {*/
                Debug.Log("You win!");
                endscreen.dayendscreen();
                reason.displaySleepHours();
           // }

        } 



		//turning off all the hitboxes
		GameObject[] clickables = GameObject.FindGameObjectsWithTag ("Clickable");
		for (int i = 0; i < clickables.Length; i++) {
			//tries to disable boxcolliders and if fails tries polygons and if fails does nothing
			try {
				clickables[i].GetComponent<BoxCollider2D>().enabled =!clickables[i].GetComponent<BoxCollider2D>().enabled;
			} catch {
				try {
					clickables[i].GetComponent<PolygonCollider2D>().enabled =!clickables[i].GetComponent<PolygonCollider2D>().enabled;
				} catch {
				}
			}
		}
    }
	public static bool playedDefeat;
    //!x is defeat, x is victory
    //the string should be "mother" or "bedtime" or "stress" based on what resulted in loss
    static public void end(bool x, string loseReason)
    {
        loadReferences();
		timer.pause ();
        if (!x)
        {
			Debug.Log("Defeat");
			if (!playedDefeat) {
				endscreen.losescreen ();
				playedDefeat = true;
			}
            if (loseReason.Equals("mother"))
            {
                reason.displayMotherLoss();
            } else if (loseReason.Equals("bedtime"))
            {
                reason.displayBedtimeLoss();
            } else if (loseReason.Equals("stress"))
            {
                reason.displayStressLoss();
            }
        } else 
        {
            //THIS SHOULD NEVER HAPPEN
            Debug.Log("This should not happen.");
            endscreen.winscreen();
            reason.errorMethod();
        }

        //turning off all the hitboxes
        GameObject[] clickables = GameObject.FindGameObjectsWithTag("Clickable");
        for (int i = 0; i < clickables.Length; i++)
        {
            //tries to disable boxcolliders and if fails tries polygons and if fails does nothing
            try
            {
                clickables[i].GetComponent<BoxCollider2D>().enabled = !clickables[i].GetComponent<BoxCollider2D>().enabled;
            }
            catch
            {
                try
                {
                    clickables[i].GetComponent<PolygonCollider2D>().enabled = !clickables[i].GetComponent<PolygonCollider2D>().enabled;
                }
                catch { }
            }
        }
    }

	public static void win() {
		//ACTIVATE WIN SCREEN HERE
		timer.pause ();
        endscreen.winscreen();
        reason.displaySleepHours();

    }

    //LOAD REFERENCES
    static void loadReferences()
    {
        screen = GameObject.Find("EndScreen");
        endscreen = (EndScreenController)screen.GetComponent(typeof(EndScreenController));
        losejustification = GameObject.Find("Justification Text");
        reason = (LoseReason)losejustification.GetComponent(typeof(LoseReason));
		timer = GameObject.Find ("TimeManager").GetComponent<TimeManager>();
    }
}
