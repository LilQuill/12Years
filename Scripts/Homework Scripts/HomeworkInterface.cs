using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS FILE CREATES AND DELETES HOMEWORK AS WELL AS CONTROLS THE ANIMATION

public class HomeworkInterface : MonoBehaviour
{

    public GameObject[] homeworkList; //holds all HW prefabs
    public GameObject startPos; //HWTransform in hierarchy
    public int HWAmount; //total homework sheets;
    GameObject homework; //placeholder
    [HideInInspector]
    public bool active; //is it active?
    [HideInInspector]
    public bool created; //is a homework created
    [HideInInspector]
    public int HWLeft;
    GameObject[] clickables; //disabling hitboxes
	[HideInInspector]
	GameObject homeworkInstantiated;
	public bool HWAmntSet;
	public bool submitted; //This variable is the reverse of what its name is. oh well.

	float destroyTime = 0.9f;
    float speed = 10f; //movement speed
    Vector3 offset; //offset of HWInterface object
    float realSpeed; //temp variable
	bool ended;
    TimeManager timer;
	SpriteRenderer grade;
	HWCounter counter;

	float changeTime;
	bool timeGot;
    public int HWCount;



    void Start()
    {
        offset = new Vector2(transform.parent.position.x, transform.parent.position.y);
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimeManager>();
        realSpeed = speed;
        //HWLeft = HWAmount;
		if (HWAmntSet == true) {
			
			HWLeft = HWAmount;
            HWCount = HWAmount;

		}
        transform.position.Set(0 + offset.x, -10 + offset.y, 0);
        clickables = GameObject.FindGameObjectsWithTag("Clickable");
		grade = transform.GetChild (3).gameObject.GetComponent<SpriteRenderer>();
		counter = GameObject.Find ("Homework Counter").GetComponent<HWCounter> ();

    }

    void Update()
    {
		
		 //has animation gone
		hwAnimate ();

        if (homeworkDone() && !ended)
        {
            EndLevel.end(true);
			ended = true;
        }

    }

    public void createHW()
    { //CREATES HOMEWORK
        homework = homeworkList[(Random.Range(0, homeworkList.Length))];
		try {
			Destroy(homeworkInstantiated.gameObject, 0);
		}
		catch {
		}
        homeworkInstantiated = Instantiate(homework, startPos.transform.position, Quaternion.identity);
        homeworkInstantiated.transform.SetParent(this.gameObject.transform);

        created = true;
    }

    public void toggleHitboxes()
    { //TOGGLES HITBOXES
      //gets all clickable tagged objects
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
                catch
                {
                }
            }
        }
    }

	public void hwSubmitted(bool x) {
		toggleHitboxes ();
		active = false;

		HWLeft--;
		HWCount--;
		counter.updateText ();
		created = false;
		try {
		Destroy(homeworkInstantiated.gameObject, destroyTime);
		}
		catch {
		}
		
		
	}

    public bool homeworkDone()
    {
        return HWLeft <= 0;
    }




    public int getHWAmount()
    {
        return HWCount;
    }

	void hwAnimate() {
		//THIS IS ALL ANIMATION
		if (active == true)
		{
			timeGot = false;
			grade.enabled = false;
			Vector3 origin = new Vector3(0 + offset.x, 0 + offset.y, 0);
			if (transform.position.y < -4.79)
			{
				transform.Translate(0, realSpeed * Time.deltaTime, 0);
				realSpeed = Mathf.Max(speed * ((((transform.position.y - offset.y - 2) * (transform.position.y - offset.y - 2)) / (speed))) + 3, 1f);
				
			}
			else
			{
				submitted = false;
				transform.position = origin;
			}
		}
		else
		{
			if (!timeGot && submitted) {
				changeTime = Time.time;
				
				timeGot = true;
			} 
			Vector3 origin = new Vector3(0 + offset.x, -15, 0);
			if (transform.position.y > -15)
			{
				if (changeTime + 0.5 < Time.time) {
					transform.Translate (0, -realSpeed * Time.deltaTime, 0);
					realSpeed = Mathf.Max (speed * ((((transform.position.y - offset.y - 2) * (transform.position.y - offset.y - 2)) / (speed))) + 3, 1f);
				}
			}
			else
			{
				grade.enabled = false;
				transform.position = origin;
				try {
					transform.GetChild (5).GetChild (1).GetChild (0).GetChild (1).GetComponent<Typable> ().setSubmissionComplete (false);
				} catch {
				}
			}
		}
	}
}
