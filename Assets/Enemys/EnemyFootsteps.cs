using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioClip footstepClip;  // 足音のオーディオクリップ
    public float footstepVolume = 0.5f;  // 足音の音量

    private AudioSource audioSource;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       
    }

    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footstepClip, footstepVolume);
    }
}

