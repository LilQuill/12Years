using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkSpriteController : MonoBehaviour {

    //REFERENCES
    private SpriteRenderer render;
    private GameObject hw;
    private HomeworkInterface hwint;
    [SerializeField] private int amount;
    [SerializeField] private Sprite[] levels;
    [SerializeField] private Sprite[] highlightedLevels;

    //BACKGROUND
    private bool mouseover;

    //INITIALIZATION
    void Start ()
    {
        render = GetComponent<SpriteRenderer>();
        hw = GameObject.Find("HomeworkInterface");
        hwint = (HomeworkInterface)hw.GetComponent(typeof(HomeworkInterface));
	}

    //REFRESH
    private void FixedUpdate() //fixed time (~.02s)
    {
        amount = hwint.HWLeft;
        if (mouseover) {
            for (int i = 0; i <= amount; i++) { render.sprite = highlightedLevels[i]; } }
        else {
            for (int i = 0; i <= amount; i++) { render.sprite = levels[i]; } }
    }

    //MOUSE HIGHLIGHTS
    private void OnMouseOver()
    { mouseover = true; }

    private void OnMouseExit()
    { mouseover = false; }

}
