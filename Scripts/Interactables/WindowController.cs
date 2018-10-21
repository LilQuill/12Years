using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {

    //REFERENCES
    private Animator anim;
    private GameObject clock;
    private TimeManager timer;
    private BoxCollider2D hitbox;

    //CONTROLLERS
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    [SerializeField] private float timeHeld;
    public int patience;

    //BACKGROUND
    private bool clickable;
    private int entertime;
    private bool hasCome;
    private bool mouseover;
    private float temp;
    private GameObject speechBubble;
    private SpriteRenderer speechBubbleRenderer;
    private GameObject student;
    private Animator studentAnimator;

    //INITIALIZATION
    void Start ()
    {
        temp = 0;
        anim = GetComponent<Animator>();
        clock = GameObject.Find("TimeManager");
        timer = (TimeManager)clock.GetComponent(typeof(TimeManager));
        clickable = false;
        hasCome = false;
        anim.SetBool("rising", false);
        entertime = Random.Range(startHour, endHour + 1);
        Debug.Log("Friend Entertime: " + entertime);
        speechBubble = GameObject.Find("SpeechBubble");
        speechBubbleRenderer = speechBubble.GetComponent<SpriteRenderer>();
        student = GameObject.Find("Student");
        studentAnimator = student.GetComponent<Animator>();
    }



    //REFRESH
    private void FixedUpdate()
    {
        friendMovement();
        changeSkyLighting();
        destress();
        timeHeldCounter();
    }

    //counting timeheld
    private void timeHeldCounter()
    {
        if (Input.GetMouseButton(0) && mouseover && clickable)
        {
            timeHeld += .02f;
        }
        else
            timeHeld = 0;

        if (timeHeld >= 5)
        {
            friendDown();
        }
    }

    //destressing function
    private void destress()
    {
        if (Input.GetMouseButton(0) && mouseover && clickable)
        {
            changeStat();
            speechBubbleRenderer.enabled = true;
            studentAnimator.SetBool("talkingToFriend", true);
        }
    }

    //subtracts the stress every second
    private void changeStat()
    {
        temp += Time.deltaTime;
        if (temp >= 1)
        {
            BackgroundStats.changeStress(-2);
            temp = 0;
        }
    }



    //MAIN FRIEND CONTROLLER FUNCTION
    private void friendMovement()
    {
        if (timer.GetHour() == entertime && !hasCome)
        {
            friendInit();
        }
        if (timer.GetHour() >= entertime + patience)
        {
            friendDown();
        }
    }

    //FRIEND FUNCTIONS
    private void friendInit()
    {
        clickable = true;
        hasCome = true;
        anim.SetBool("rising", true);
        anim.SetBool("friend", true);
    }

    private void friendDown()
    {
        anim.SetBool("friend", false);
        clickable = false;
        anim.SetBool("rising", false);
        speechBubbleRenderer.enabled = false;
        studentAnimator.SetBool("talkingToFriend", false);
    }



    //MOUSE FUNCTIONS
    private void OnMouseOver()
    {
        if (clickable) {
			anim.SetLayerWeight (1, 1);
			mouseover = true;
		}
    }

    private void OnMouseExit()
    {
        anim.SetLayerWeight(1, 0);
        mouseover = false;
    }

    private void OnMouseUp()
    {
        if (mouseover)
        {
            friendDown();   
        }
        
    }

    //CHANGING THE SKY LIGHTING DEPENDING ON HOURS BEFORE BEDTIME
    private void changeSkyLighting()
    {
        if (timer.GetHour() > timer.endHour - 3)
        {
            anim.SetBool("darken", true);
        }
        else
        {
            anim.SetBool("darken", false);
        }
    }
}
