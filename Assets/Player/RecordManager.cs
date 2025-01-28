using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public AudioClip audioClip;  // �Đ�����I�[�f�B�I�N���b�v

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource ��������
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;  // ���������[�v������
    }

    // �I�u�W�F�N�g�ݒu�ʒu�ŉ�����ݒ�
    public void SetAudioSource(Vector3 position)
    {
        if (audioSource != null)
        {
            // AudioSource �̈ʒu��ύX
            audioSource.transform.position = position;

            // �����Đ�
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);  // ������1�񂾂��Đ�
            }
        }
    }
}
