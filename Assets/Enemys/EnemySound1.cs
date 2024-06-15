using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound1 : MonoBehaviour
{
    public AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 40f;          // ����

    private void Start()
    {
        audioSource.clip = footstepSound;
        audioSource.loop = true;
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
