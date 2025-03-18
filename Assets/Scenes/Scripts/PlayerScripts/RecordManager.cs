using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public AudioClip audioClip;  // �Đ�����I�[�f�B�I�N���b�v

    // Start is called before the first frame update
    void Start()
    {

    }

    // �I�u�W�F�N�g�� AudioSource ��ǉ����ĉ�����ݒ�
    public void SetAudioSource(GameObject placedObject)
    {
        AudioSource placedAudioSource = placedObject.GetComponent<AudioSource>();
        if (placedAudioSource == null)
        {
            placedAudioSource = placedObject.AddComponent<AudioSource>();
        }

        // �����ݒ�
        placedAudioSource.clip = audioClip;
        placedAudioSource.spatialBlend = 1.0f;  // 3D ����
        placedAudioSource.volume = 0.1f;  // ���ʂ�0.1�ɐݒ�
        placedAudioSource.loop = false;  // ���[�v���I�t
        placedAudioSource.rolloffMode = AudioRolloffMode.Linear;  // ���[���I�t��Linear�ɐݒ�
        placedAudioSource.minDistance = 0f;  // �ŏ�������0�ɐݒ�

        placedAudioSource.Play();  // �Đ�
    }
}