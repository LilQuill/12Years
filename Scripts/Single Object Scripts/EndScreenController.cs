using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour {

    //GAMEOBJECT REERENCESS
    private GameObject musicplayer;
    private MusicController music;

    //SELF REFERENCES
    private SpriteRenderer render;
    [SerializeField] private Sprite win;
    [SerializeField] private Sprite lose;
    [SerializeField] private Sprite dayEnd;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private AudioSource audioSource;
    private GameObject txt;
    private GameObject nextButton;

    //INITIALIZATION
    void Start()
    {
        nextButton = transform.GetChild(0).gameObject;
        nextButton.SetActive(false);
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
        musicplayer = GameObject.Find("Music Player");
        music = musicplayer.GetComponent<MusicController>();
        audioSource = GetComponent<AudioSource>();
        txt = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    public void losescreen()
    {
        nextButton.SetActive(true);
        render.sprite = lose;
        render.enabled = true;
        audioSource.clip = loseSound;
        audioSource.Play();
        music.stopMusic();
        txt.GetComponent<RetryContinueText>().retryMode();
    }

    public void winscreen()
    {
        nextButton.SetActive(true);
        render.sprite = win;
        render.enabled = true;
        audioSource.clip = winSound;
        audioSource.Play();
        music.playWinMusic();
		txt.GetComponent<RetryContinueText>().winMode();
        //change this to go to main menu instead of Retry
    }

    public void dayendscreen()
    {
        nextButton.SetActive(true);
        render.sprite = dayEnd;
        render.enabled = true;
        audioSource.clip = winSound;
        audioSource.Play();
        music.playWinMusic();
        txt.GetComponent<RetryContinueText>().continueMode();
    }
}
