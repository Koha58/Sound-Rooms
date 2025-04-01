using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �s�A�m�̉����~���[�g�ɂ�����N���X
/// </summary>
public class PianoMute : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource �R���|�[�l���g���i�[����ϐ�
    PlayerSeen PS;  // PlayerSeen �X�N���v�g���i�[����ϐ�

    // Start �̓X�N���v�g���J�n�����Ƃ��ɍŏ��Ɏ��s�����
    private void Start()
    {
        // AudioSource �R���|�[�l���g�����̃I�u�W�F�N�g����擾
        audioSource = GetComponent<AudioSource>();

        // "Player" �I�u�W�F�N�g���V�[�����猟�����Ď擾
        GameObject Player = GameObject.Find("Player");

        // "Player" �I�u�W�F�N�g�ɃA�^�b�`����Ă��� PlayerSeen �X�N���v�g���擾
        PS = Player.GetComponent<PlayerSeen>();

        // ������Ԃŉ������~���[�g�ɐݒ�
        audioSource.mute = true;
    }

    // Update �͖��t���[���Ăяo�����
    void Update()
    {
        // PlayerSeen �X�N���v�g���� piano �ϐ��� false �̏ꍇ�A�������~���[�g
        if (PS.piano == false)
        {
            audioSource.mute = true;  // �~���[�g
        }
        else
        {
            // piano �� true �̏ꍇ�A�������~���[�g����
            audioSource.mute = false;  // �~���[�g����
        }
    }
}
