using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Sound1 : MonoBehaviour
{
    // public AudioClip Sound;     // �����̃I�[�f�B�I�N���b�v
    public AudioClip Sound1;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 40f;          // ����

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound1;
        GameObject eobj3 = GameObject.FindWithTag("Enemy3");
        EnemyController3 EC3 = eobj3.GetComponent<EnemyController3>();
        if (EC3.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC3.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
