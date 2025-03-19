using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �v���C���[�̉����E�s�������Ǘ�����N���X
/// </summary>
public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  // ����p�i�v���C���[�������Ă��Ȃ����F0 / �����Ă��鎞�F1�j

    [SerializeField] public Transform _parentTransform; // �v���C���[�̐e�I�u�W�F�N�g
    LevelMeter levelMeter; // ���ʂ𑪒肷��X�N���v�g
    public bool piano; // �s�A�m�����ł̋�������t���O
    int pianocnt; // �s�A�m�����̋����Ɋւ���J�E���^
    public bool zero; // �s�A�m�����̉��ʂ��[�����ǂ����𔻒肷��t���O
    AudioSetting AS; // ���ʐݒ���Ǘ�����X�N���v�g

    public bool Visualization; // �v���C���[�������邩�ǂ����̏��

    void Start()
    {
        // ������Ԃ̐ݒ�
        onoff = 0;  // �v���C���[�͍ŏ������Ă��Ȃ�
        Visualization = false;  // �����̏�Ԃ͏����͕s��
        piano = false;  // �s�A�m�����łȂ�
        pianocnt = 0;  // �s�A�m�����̋����J�E���g
        zero = false;  // ���ʃ[���t���O�͏����̓I�t
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeter�X�N���v�g���擾

        // �����o�����ƂŃv���C���[��������悤�ɂȂ�
        if (levelMeter.nowdB > 0.0f && !piano)
        {
            onoff = 1;  // �����Ă����ԂɕύX
        }

        if (Visualization == false)
        {
            // �����o���Ă��Ȃ��ꍇ�A�v���C���[�������Ȃ�����
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f && !piano)
                {
                    onoff = 0;  // �����Ă��Ȃ���ԂɕύX
                }
            }
        }

        // �s�A�m�����̋����Ǘ�
        if (piano)
        {
            onoff = 1;  // �s�A�m�����ł̓v���C���[�͌�����

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>(); // AudioSetting�X�N���v�g���擾

            // ���ʂ��ŏ��i-80�j�̏ꍇ�A�s�A�m�����̋������I��
            if (AS.BGMSlider.value == -80)
            {
                zero = true;  // ���ʃ[�������o
                piano = false;  // �s�A�m�����I��
                onoff = 0;  // �����Ȃ���Ԃɖ߂�
            }
            else
            {
                piano = true;  // �s�A�m����
                zero = false;  // ���ʃ[���t���O�I�t
                onoff = 1;  // �v���C���[�͌�����
            }
        }
        else
        {
            zero = false;  // �s�A�m�����łȂ��ꍇ
            // �s�A�m�����̋����J�E���^����Ȃ�s�A�m������Ԃɂ���
            if (pianocnt % 2 != 0 && AS.BGMSlider.value != -80)
            {
                piano = true;
            }
        }
    }

    // �v���C���[���uPianoCheck�v�^�O�̃I�u�W�F�N�g�ƏՓ˂�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            pianocnt++;  // �s�A�m���������J�E���^�𑝉�
            if (!zero)
            {
                piano = true;  // �s�A�m������Ԃɂ���

                // �J�E���g�������Ȃ�s�A�m�������I��
                if (pianocnt % 2 == 0)
                {
                    piano = false;
                }
            }
        }
    }

    // �v���C���[���uRoomOut�v�^�O�̃I�u�W�F�N�g���ɂ���Ƃ�
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RoomOut"))
        {
            onoff = 0;  // �v���C���[�͌����Ȃ���Ԃɖ߂�
        }
    }
}