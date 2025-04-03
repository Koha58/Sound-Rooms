using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ʃ��x�����������[�^�[�i���x�����[�^�[�j���Ǘ�����N���X
/// </summary>
public class LevelMeter : MonoBehaviour
{
    // �X�V����Ώۂ�levelMeter (uGUI Image)
    Image levelMeterImage = null;

    // ����dB��levelMeter�\���̉����ɓ��B����
    [SerializeField]
    private float dB_Min = -60.0f;  // �ŏ�dB�i���ʂ̉����j

    // ����dB��levelMeter�\���̏���ɓ��B����
    [SerializeField]
    private float dB_Max = -0.0f;   // �ő�dB�i���ʂ̏���j

    // dB���擾����Ώۂ�micAudioSource
    [SerializeField]
    public MicAudioSource micAS = null;  // �}�C�N�����̃f�[�^���擾����MicAudioSource

    public float nowdB;  // ���݂�dB�l

    // �Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂȂ�O�ɌĂ΂��
    void Awake()
    {
        // �X�V����Ώۂ�Image�i���x�����[�^�[��UI�j���擾
        levelMeterImage = GetComponent<Image>();
    }

    void Start()
    {
        // MicAudioSource�R���|�[�l���g���V�[������擾
        micAS = FindObjectOfType<MicAudioSource>();
    }

    void Update()
    {
        // micAS���猻�݂�dB�l���擾���A�����fillAmount�ɕϊ�
        float fillAmountValue = dB_ToFillAmountValue(micAS.now_dB);

        // ���x�����[�^�[��fillAmount���X�V�i�\���̐i����j
        this.levelMeterImage.fillAmount = fillAmountValue;

        // ���݂�dB�l���i�[
        nowdB = fillAmountValue;

        // dB��0���傫����΁A���x�����[�^�[�̐F��ύX�i���ʂ��傫���ꍇ�j
        if (nowdB > 0f)
        {
            // ���x�����[�^�[�����ʂɉ����ĐF��ύX
            levelMeterImage.color = new Color32(255, 255, 255, 154);
        }
    }

    /// <summary>
    /// dB_Min��dB_Max�Ɋ�Â���dB��fillAmount�l�ɕϊ�
    /// </summary>
    /// <param name="dB">���݂�dB�l</param>
    /// <returns>fillAmount�l�i0.0f����1.0f�͈̔́j</returns>
    float dB_ToFillAmountValue(float dB)
    {
        // ���͂��ꂽdB��dB_Max��dB_Min�l�Ő؂�̂āi�͈͓��Ɏ��߂�j
        float modified_dB = dB;
        if (modified_dB > dB_Max) { modified_dB = dB_Max; }   // dB���ő�l���傫����΍ő�l�ɐݒ�
        else if (modified_dB < dB_Min) { modified_dB = dB_Min; }  // dB���ŏ��l��菬������΍ŏ��l�ɐݒ�

        // dB��fillAmount�i0.0f����1.0f�j�͈̔͂ɕϊ�
        // dB_Min = 0.0f, dB_Max = 1.0f �Ƃ����ϊ���
        float fillAountValue = 1.0f + (modified_dB / (dB_Max - dB_Min));
        return fillAountValue;  // �v�Z����fillAmount�l��Ԃ�
    }
}