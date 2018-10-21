using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedtimeFilterController : MonoBehaviour {

	public float topOpacity = .60f; //opacity of filter when activated
	public float changeTime = 1f;
	float opacity;
	bool bedtime = false;
	SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render = gameObject.GetComponent<SpriteRenderer> ();
		render.enabled = true;
		opacity = topOpacity;
		render.color = new Color (1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (bedtime && render.color.a <= topOpacity) {
			render.color = new Color(1f, 1f, 1f, render.color.a + Time.deltaTime/changeTime);
		} else if(!bedtime && render.color.a >= 0) {
			render.color = new Color(1f, 1f, 1f, render.color.a - Time.deltaTime/changeTime);
		}
	}

	public void setBedtime(bool x) {
		bedtime = x;
	}
}
