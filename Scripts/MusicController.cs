using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip[] tracks;
    private AudioSource audioSource;
    private AudioLowPassFilter filter;
    private bool quiet;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        filter = GetComponent<AudioLowPassFilter>();
        filter.enabled = false;
        quiet = false;
        audioSource.volume = .5f;
    }

    //plays menu music
    public void playMenuMusic()
    {
        filter.enabled = false;
        audioSource.volume = .5f;
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    //play main game music
    public void playMusic()
    {
		AudioClip temp = tracks[Random.Range(0, tracks.Length-1)];
        filter.enabled = false;
        audioSource.volume = .25f;
        //		Debug.Log (temp);
        //		Debug.Log (audioSource.clip);
        audioSource.clip = temp;
        audioSource.Play();
    }

    //plays lose music
    public void playLoseMusic()
    {
        filter.enabled = false;
        audioSource.volume = .5f;
        audioSource.clip = loseMusic;
        audioSource.Play();
    }

    //plays win music
    public void playWinMusic()
    {
        filter.enabled = false;
        audioSource.volume = .5f;
        audioSource.clip = winMusic;
        audioSource.Play();
    }

    //stops any music
    public void stopMusic()
    {
        audioSource.Stop();
    }

    //FOR PAUSE MENU
    public void toggleQuietMusic()
    {
        if (!quiet)
        {
            filter.enabled = true;
            audioSource.volume = .1f;
        }
        else
        {
            filter.enabled = false;
            audioSource.volume = .25f;
        }
        quiet = !quiet;
    }
}
