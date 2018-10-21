using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip mouseSound;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        audioSource.clip = mouseSound;
        audioSource.Play();
    }

    private void OnMouseDown()
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
