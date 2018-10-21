using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour {

    private SpriteRenderer render;
    private float loadspeed;
    private Animator anim;
    public bool click;

    //INITIALIZATION
	void Start () {
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        anim = GetComponent<Animator>();
	}

    //Loading bar disappearing after completing
    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 || !click)
        {
            render.enabled = false;
        }
    }

    //Loading bar for the laundry
    public void loadLaundry(float time)
    {
        changeFPS(time);
        transform.SetPositionAndRotation(new Vector3(-7.29f, -3.45f, 0), Quaternion.identity);
        anim.Play("Loading", -1, 0f);
        render.enabled = true;
        click = true;
    }

    //Loading bar for the trash
    public void loadTrash(float time)
    {
        changeFPS(time);
        transform.SetPositionAndRotation(new Vector3(5.36f, -2f, 0), Quaternion.identity);
        anim.Play("Loading", -1, 0f);
        render.enabled = true;
        click = true;
    }

    //closes the loading bar
    public void stopLoading()
    {
        click = false;
        render.enabled = false;
    }

    //Loading bar loads at different speeds based on the time it takes to load
    private void changeFPS(float time)
    {
        anim.SetFloat("speed", (float)1 / time);
    }
}
