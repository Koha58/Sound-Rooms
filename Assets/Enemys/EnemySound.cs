using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip[] audioClips; // �Đ����鉹���̔z��
    private AudioSource audioSource;

    private int currentClipIndex = 0; // ���ݍĐ����̉����̃C���f�b�N�X

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>();
        if (EC.ONoff == 0)
        {
            PlayNextClip();
        }
        if (EC.ONoff == 1)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        // ���̉����̃C���f�b�N�X���X�V����
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // ���̉������I�[�f�B�I�\�[�X�ɐݒ肵�čĐ�����
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }
}