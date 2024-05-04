using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 0.5f;          // ����

    private void Start()
    {
      
    }

    void Update()
    {
        if (EnemyChase.detectionPlayer <= EnemyChase.Detection)
        {
            audioSource.clip = footstepSound;
            audioSource.loop = true;
        }
    }
        // �������Đ����郁�\�b�h
    public void PlayFootstepSound()
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    // �����̍Đ����~���郁�\�b�h
    public void StopFootstepSound()
    {
        audioSource.Stop();
    }
}

