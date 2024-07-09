using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Sound1 : MonoBehaviour
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
        GameObject eobj5 = GameObject.FindWithTag("Enemy5");
        EnemyController5 EC5 = eobj5.GetComponent<EnemyController5>();
        if (EC5.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC5.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
