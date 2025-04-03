using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player���ݒu���郉�W�I�̍Đ��Ǘ��N���X
/// </summary>
public class RecordManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;  // �Đ�����I�[�f�B�I�N���b�v

    // �I�u�W�F�N�g�� AudioSource ��ǉ����ĉ�����ݒ�
    public void SetAudioSource(GameObject placedObject)
    {
        // ������ AudioSource ���擾�B�Ȃ���ΐV���� AudioSource ��ǉ�
        AudioSource placedAudioSource = placedObject.GetComponent<AudioSource>();
        if (placedAudioSource == null)
        {
            placedAudioSource = placedObject.AddComponent<AudioSource>();  // AudioSource �R���|�[�l���g���Ȃ��ꍇ�A�ǉ�����
        }

        // �����ݒ�
        placedAudioSource.clip = audioClip;  // �Đ�����I�[�f�B�I�N���b�v��ݒ�
        placedAudioSource.spatialBlend = 1.0f;  // 3D�����Ƃ��Đݒ�B1.0f�͊��S��3D����
        placedAudioSource.volume = 0.1f;  // ���ʂ�0.1�ɐݒ�i�����߂̉��ʁj
        placedAudioSource.loop = false;  // ���������[�v���Ȃ��悤�ɐݒ�
        placedAudioSource.rolloffMode = AudioRolloffMode.Linear;  // ���̌������[�h����`�����ɐݒ�
        placedAudioSource.minDistance = 0f;  // �ŏ�������0�ɐݒ�B�����Ƃ̋�����0�ł����ʂ��������Ȃ�

        placedAudioSource.Play();  // �ݒ肵���������Đ�
    }
}