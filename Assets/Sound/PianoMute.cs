using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoMute : MonoBehaviour
{
    private AudioSource audioSource;
    PlayerSeen PS;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject Player = GameObject.Find("Player");
        PS = Player.GetComponent<PlayerSeen>();
        audioSource.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(PS.piano == false)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
}
