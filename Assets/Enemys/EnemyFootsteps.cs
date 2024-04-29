using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioClip footstepClip;  // �����̃I�[�f�B�I�N���b�v
    public float footstepVolume = 0.5f;  // �����̉���

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

