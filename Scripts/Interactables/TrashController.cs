using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

    //REFERENCES
    private Animator anim;
    private GameObject clock;
    private TimeManager timer;
    private GameObject loadingbar;
    private LoadingController load;

    //SPEED CONTROLLERS
    public float timeToFull;
    public float emptySpeed = 5;

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

        lastEmpty = 0;
        full = false;
        anim.SetBool("trash full", false);
    }


    //REFRESH
    void FixedUpdate() //every fixed amount of time (.02?)
    {
        if (full == true)
        {
            anim.SetBool("trash full", true);
        }
        emptyTrash();
    }

    private void Update() //every frame
    {
        if ((timer.GetTimeElapsed() - lastEmpty) > timeToFull)
        {
            full = true;
        }
    }


    //CLICK AND HOLD EMPTYING
    private void emptyTrash()
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
                anim.SetBool("trash full", false);
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
            if(!load.click)
                load.stopLoading();
        }
    }

    //MOUSEOVER FUNCTIONS
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
        load.loadTrash(emptySpeed);
    }

    private void OnMouseUp()
    {
        load.stopLoading();
    }


    //PUBLIC FUNCTIONS
    public bool IsTrashFull()
    {
        return full;
    }

    //SETS GROWTH FPS BASED ON var timeToFull 
    private void changeFps()
    {
        anim.SetFloat("anim speed", (float)(.666666 / timeToFull));
    }
}
