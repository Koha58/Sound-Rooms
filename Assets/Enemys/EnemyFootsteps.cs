using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}

