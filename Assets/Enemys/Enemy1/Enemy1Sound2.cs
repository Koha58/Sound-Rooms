using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Sound2 : MonoBehaviour
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
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
        if (EC1.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC1.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
