using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Sound2 : MonoBehaviour
{
    // public AudioClip Sound;     // �����̃I�[�f�B�I�N���b�v
    public AudioClip Sound2;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 40f;          // ����

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound2;
        GameObject eobj2 = GameObject.FindWithTag("Enemy2");
        EnemyController2 EC2 = eobj2.GetComponent<EnemyController2>();
        if (EC2.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC2.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
