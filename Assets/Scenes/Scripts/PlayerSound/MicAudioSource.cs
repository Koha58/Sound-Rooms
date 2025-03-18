using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MicAudioSource : MonoBehaviour
{
    //�T���v�����O���g��
    static readonly int SAMPLE_RATE = 48000;

    //���̕b���̕��ŐU���̕��ϒl����������̂�dB�l���X�V
    static readonly float MOVING_AVE_TIME = 0.05f;

    //MOVING_AVE_TIME�ɑ�������T���v����
    static readonly int MOVING_AVE_SAMPLE = (int)(SAMPLE_RATE * MOVING_AVE_TIME);

    //�}�C�N��Clip���Z�b�g����ׂ�AudioSource
    AudioSource micAS = null;

    //���݂�dB�l
    private float _now_dB;
    public float now_dB { get { return _now_dB; } }

    private void Awake()
    {
        //AudioSource�R���|�[�l���g�擾
        micAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        //�ŏ��Ƀ}�C�N�̓��͂��J�n
        this.MicStart();
    }

    // �}�C�N���͂̊J�n
    public void MicStart()
    {
        // AudioSource��Clip�Ƀ}�C�N�f�o�C�X���Z�b�g
        micAS.clip = Microphone.Start(null, true, 1, SAMPLE_RATE);

        // �}�C�N�f�o�C�X�̏������ł���܂ő҂�
        while (!(Microphone.GetPosition("") > 0)) { }

        // AudioSource����̏o�͂��J�n
        micAS.Play();
    }

    // MicAudioSource �N���X
    void Update()
    {
        if (micAS.isPlaying)
        {
            // GetOutputData �p�̃o�b�t�@������
            float[] data = new float[MOVING_AVE_SAMPLE];

            // AudioSource ����o�͂���Ă���T���v�����擾
            micAS.GetOutputData(data, 0);

            // �o�b�t�@���̕��ϐU�����擾�i��Βl�𕽋ς���j
            float aveAmp = data.Average(s => Mathf.Abs(s));

            // �U���� dB�i�f�V�x���j�ɕϊ�
            float dB = 20.0f * Mathf.Log10(aveAmp);

            // ���ݒl�inow_dB�j���X�V
            _now_dB = dB;
        }
    }
}
