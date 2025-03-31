using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �}�C�N�̓��͂��Ǘ�����N���X
/// ���ʁidB�j�����A���^�C���Ŏ擾���A���̃R���|�[�l���g�ŗ��p�ł���悤�ɂ���
/// </summary>
public class MicAudioSource : MonoBehaviour
{
    // �T���v�����O���g�� (48kHz)
    static readonly int SAMPLE_RATE = 48000;

    // ���̕b���̕��ŐU���̕��ϒl����������̂�dB�l���X�V
    // ���ʁidB�j�̕ω����Ȃ߂炩�ɂ��邽�߂Ɉ�莞�Ԃ��ƂɍX�V����
    static readonly float MOVING_AVE_TIME = 0.05f;

    // MOVING_AVE_TIME�ɑ�������T���v����
    static readonly int MOVING_AVE_SAMPLE = (int)(SAMPLE_RATE * MOVING_AVE_TIME);

    // �}�C�N�̉������������߂�AudioSource�R���|�[�l���g
    AudioSource micAS = null;

    // ���݂�dB�l
    private float _now_dB;  // �v���C�x�[�g�ȕϐ��ŁA�O���A�N�Z�X�p�̃v���p�e�B���
    public float now_dB { get { return _now_dB; } }  // �O�����猻�݂�dB�l���擾���邽�߂̃v���p�e�B


    private void Awake()
    {
        // AudioSource�R���|�[�l���g���擾
        micAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        // �ŏ��Ƀ}�C�N�̓��͂��J�n
        this.MicStart();
    }

    // �}�C�N���͂̊J�n
    public void MicStart()
    {
        // �}�C�N�f�o�C�X��AudioSource��Clip�ɃZ�b�g
        // Microphone.Start�Ń}�C�N�̘^�����J�n
        micAS.clip = Microphone.Start(null, true, 1, SAMPLE_RATE);

        // �}�C�N�f�o�C�X�̏������ł���܂őҋ@�i�}�C�N���特���擾�\�ɂȂ�̂�҂j
        while (!(Microphone.GetPosition("") > 0)) { }

        // �}�C�N�̉����̍Đ����J�n
        micAS.Play();
    }

    void Update()
    {
        if (micAS.isPlaying)  // �}�C�N���Đ����ł����
        {
            // GetOutputData�p�̃o�b�t�@������
            // �����f�[�^���擾���邽�߂̔z��
            float[] data = new float[MOVING_AVE_SAMPLE];

            // AudioSource����o�͂���Ă���T���v���f�[�^���擾
            // �����Ŏ擾�����f�[�^�́A�}�C�N�̓��͉����̐U�����
            micAS.GetOutputData(data, 0);

            // �o�b�t�@���̕��ϐU�����擾�i�e�U���̐�Βl����蕽�ϒl���v�Z�j
            // �U���̕��ς��g�����ƂŁA���ʂ̃s�[�N�l�ł͂Ȃ����ϓI�ȉ��ʂ��擾
            float aveAmp = data.Average(s => Mathf.Abs(s));

            // �U�������ɏ������i�[���ɋ߂��j�ꍇ�ł������E�����߁A�ŏ��U����ݒ�
            if (aveAmp < 0.0001f) // ������0.0001�͔��ɏ��������ʂ�臒l
            {
                aveAmp = 0.0001f; // �U�������ɏ������ꍇ�ł�0.0001�̐U���ɐݒ�
            }

            // ���ϐU������dB�i�f�V�x���j�ɕϊ�
            _now_dB = 20.0f * Mathf.Log10(aveAmp);

            // Debug���O�i�K�v�ɉ����Ċm�F�j
            Debug.Log(_now_dB);
        }
    }
}