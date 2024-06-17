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
        StartCoroutine(PlayAudioClips());
    }

    private IEnumerator PlayAudioClips()
    {
        while (true)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length); // ���݂̉����̍Đ����I������܂ő҂�

            // �C���f�b�N�X�����̉����ɍX�V
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }
}