using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound2 : MonoBehaviour
{
    public AudioClip Sound2;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 1f;          // ����

    private void Start()
    {

    }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>();

        if (EC.ONoff == 0)
        {
            audioSource.clip = Sound2;
            audioSource.Play();
        }
        if (EC.ONoff == 1)
        {
            audioSource.Stop();
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
