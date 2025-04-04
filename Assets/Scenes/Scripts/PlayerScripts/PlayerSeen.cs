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
    // �v���C���[�̉���Ԃ��Ǘ�
    public bool isVisible = false;  // ����p�i�v���C���[�������Ă��Ȃ����Ffalse / �����Ă��鎞�Ftrue�j

    // �v���C���[�̐e�I�u�W�F�N�g
    [SerializeField] private Transform _parentTransform;

    // ���ʂ𑪒肷��X�N���v�g�̃C���X�^���X
    private LevelMeter levelMeter;

    // �s�A�m�����ł̋�������t���O
    public bool piano;

    // �s�A�m�����̋����Ɋւ���J�E���^
    private int pianocnt;

    // �s�A�m�����̉��ʂ��[�����ǂ����𔻒肷��t���O
    private bool zero;

    // ���ʐݒ���Ǘ�����X�N���v�g
    private AudioSetting AS;

    // �G�Q�Ɨp�v���C���[�������邩�ǂ����̏��
    public bool isVisualization;

    // BGM�̃~���[�g�Ƃ���l�i���ʁj
    private int muteBGM = -80;

    // �}�C�N�̓��͔���p臒l
    private float muteLevel = 0.0f;

    // ��������p�萔
    const int EvenNumber = 2;

    void Start()
    {
        // ������Ԃ̐ݒ�
        isVisible = false;  // �v���C���[�͍ŏ������Ă��Ȃ�
        isVisualization = false;  // �����̏�Ԃ͏����͕s��
        piano = false;  // �s�A�m�����łȂ�
        pianocnt = 0;  // �s�A�m�����̋����J�E���g
        zero = false;  // ���ʃ[���t���O�͏����̓I�t
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeter�X�N���v�g���擾

        // �����o�����ƂŃv���C���[��������悤�ɂȂ�
        if (levelMeter.nowdB > muteLevel && !piano)
        {
            isVisible = true;  // �����Ă����ԂɕύX
        }

        if (!isVisualization)
        {
            // �����o���Ă��Ȃ��ꍇ�A�v���C���[�������Ȃ�����
            if (isVisible)
            {
                if (levelMeter.nowdB <= muteLevel && !piano)
                {
                    isVisible = false;  // �����Ă��Ȃ���ԂɕύX
                }
            }
        }

        // �s�A�m�����̋����Ǘ�
        if (piano)
        {
            isVisible = true;  // �s�A�m�����ł̓v���C���[�͌�����

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>(); // AudioSetting�X�N���v�g���擾

            // ���ʂ��ŏ��i-80�j�̏ꍇ�A�s�A�m�����̋������I��
            if (AS.BGMSlider.value == muteBGM)
            {
                zero = true;  // ���ʃ[�������o
                piano = false;  // �s�A�m�����I��
                isVisible = false;  // �����Ȃ���Ԃɖ߂�
            }
            else
            {
                piano = true;  // �s�A�m����
                zero = false;  // ���ʃ[���t���O�I�t
                isVisible = true;  // �v���C���[�͌�����
            }
        }
        else
        {
            zero = false;  // �s�A�m�����łȂ��ꍇ
            // �s�A�m�����̋����J�E���^����Ȃ�s�A�m������Ԃɂ���
            if (pianocnt % EvenNumber != 0 && AS.BGMSlider.value != muteBGM)
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
                if (pianocnt % EvenNumber == 0)
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
            isVisible = false;  // �v���C���[�͌����Ȃ���Ԃɖ߂�
        }
    }
}