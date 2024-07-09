using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8Sound1 : MonoBehaviour
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
        GameObject eobj8 = GameObject.FindWithTag("Enemy8");
        EnemyController8 EC8 = eobj8.GetComponent<EnemyController8>();
        if (EC8.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC8.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
