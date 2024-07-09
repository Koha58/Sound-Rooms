using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9Sound1 : MonoBehaviour
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
        GameObject eobj9 = GameObject.FindWithTag("Enemy9");
        EnemyController9 EC9 = eobj9.GetComponent<EnemyController9>();
        if (EC9.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC9.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
