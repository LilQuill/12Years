using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkHitbox : MonoBehaviour {

	HomeworkInterface hitbox;
	public AudioClip[] clips;

	void Start() {
        hitbox = GameObject.Find("HomeworkInterface").GetComponent<HomeworkInterface>();
	}

	void OnMouseDown () {
		Debug.Log ("clicked???");
        if (hitbox.HWLeft > 0)
        {
			hitbox.active = !hitbox.active;
			hitbox.toggleHitboxes();
            if (!hitbox.created)
            {
				hitbox.createHW ();
			}
			AudioSource sound = transform.parent.gameObject.GetComponent<AudioSource> ();
			sound.clip = clips [Random.Range (0, clips.Length)];
			sound.Play ();
		}
	}
}
