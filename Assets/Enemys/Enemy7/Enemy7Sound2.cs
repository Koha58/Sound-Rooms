using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Sound2 : MonoBehaviour
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
        GameObject eobj7 = GameObject.FindWithTag("Enemy7");
        EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>();
        if (EC7.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC7.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
