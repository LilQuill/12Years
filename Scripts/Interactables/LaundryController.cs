using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundryController : MonoBehaviour
{

    //REFERENCES
    private Animator anim;
    private GameObject clock;
    private TimeManager timer;
    private GameObject loadingbar;
    private LoadingController load;

    //SPEED CONTROLLERS
    public float timeToFull;
    public float emptySpeed;

    //BACKGROUND
    private bool full;
    private bool mouseover;
	private bool soundPlayed;
    private float lastEmpty;
    private float timeHeld;

    //INITIALIZATION
    void Start()
    {
        anim = GetComponent<Animator>();
        clock = GameObject.Find("TimeManager");
        timer = (TimeManager)clock.GetComponent(typeof(TimeManager));
        loadingbar = GameObject.Find("Loading Bar");
        load = (LoadingController)loadingbar.GetComponent(typeof(LoadingController));
        changeFps();

        timeHeld = 0;
        lastEmpty = 0;
        full = false;
        anim.SetBool("laundry full", false);
    }


    //REFRESH
    void FixedUpdate() //fixed refresh
    {
        if (full == true)
        {
            anim.SetBool("laundry full", true);
        }
        emptyLaundry();
    }

    private void Update() //every frame
    {
        if ((timer.GetTimeElapsed() - lastEmpty) > timeToFull)
        {
            full = true;
        }
    }


    //CLICK AND HOLD EMPTYING
    private void emptyLaundry()
    {        
        if (Input.GetMouseButton(0) && mouseover)
        {
            if (timeHeld > emptySpeed)
            {
				if (soundPlayed == false) {
					GetComponent<AudioSource> ().Play ();
					soundPlayed = true;
				}
				full = false;
                anim.SetBool("laundry full", false);
                anim.Play("Growing", -1, 0f);
                anim.Play("Growing HL", -1, 0f);
                lastEmpty = timer.GetTimeElapsed();
            }
            else
            {
                timeHeld += .02f;
            }
        }
        else
        {
            timeHeld = 0;
			soundPlayed = false;
            if (!load.click)
                load.stopLoading();
        }
    }


    //MOUSE FUNCTIONS
    private void OnMouseOver()
    {
        mouseover = true;
        anim.SetLayerWeight(1, 1);
    }
    private void OnMouseExit()
    {
        mouseover = false;
        load.stopLoading();
        anim.SetLayerWeight(1, 0);
    }

    private void OnMouseDown()
    {
        load.loadLaundry(emptySpeed);
    }

    private void OnMouseUp()
    {
        load.stopLoading();
    }


    //PUBLIC FUNCTIONS
    public bool IsLaundryFull()
    {
        return full;
    }


    //SETTING GROWTH FPS BASED ON var timeToFull 
    private void changeFps()
    {
        anim.SetFloat("anim speed", (float)(.75 / timeToFull));
    }
}
