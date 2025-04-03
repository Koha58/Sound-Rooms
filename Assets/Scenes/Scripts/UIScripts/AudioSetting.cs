using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// �I�v�V������ʂŉ��ʂ�}�E�X���x�Ȃǂ𒲐����邽�߂̋@�\��񋟂���N���X
/// </summary>
public class AudioSetting : MonoBehaviour
{
    // �萔��`
    private const float MouseSensitivityDivisor = 50f;  // Y���̊��x�����p
    private const float MouseSensitivityMultiplier = 50f;  // X���̊��x�����p
    private const float DefaultMicVolume = 0.5f;  // �����̃}�C�N���ʁi�f�t�H���g�l�j

    // �I�[�f�B�I�~�L�T�[�A�}�C�N�I�u�W�F�N�g�A�{�����[���ݒ�p�̕ϐ�
    [SerializeField] AudioMixer audioMixer;      // �I�[�f�B�I�~�L�T�[
    [SerializeField] GameObject micObject;       // �}�C�N�̃I�u�W�F�N�g
    public float volume;                         // �{�����[��

    // CinemachineFreeLook �J�����i�}�E�X���x�ݒ�p�j
    public CinemachineFreeLook VCamera;

    // UI�̃X���C�_�[�i�e���ʒ����p�j
    [SerializeField] Slider MicSlider;          // �}�C�N���ʗp�X���C�_�[
    [SerializeField] public Slider BGMSlider;   // BGM���ʗp�X���C�_�[
    [SerializeField] Slider SESlider;           // SE���ʗp�X���C�_�[
    [SerializeField] Slider MouseSlider;        // �}�E�X���x�p�X���C�_�[

    // Start is called before the first frame update
    private void Start()
    {
        // �}�C�N��AudioSource�R���|�[�l���g���擾
        AudioSource Mic = micObject.GetComponent<AudioSource>();

        // �}�C�N���ʂ��X���C�_�[�ɔ��f�i�f�t�H���g�l���g�p�j
        MicSlider.value = DefaultMicVolume;

        // �}�E�X���x�̐ݒ�iVCamera��Y���̍ő呬�x���X���C�_�[�l�Ɋ�Â��Đݒ�j
        MouseSlider.value = VCamera.m_YAxis.m_MaxSpeed;

        // VCamera��X���̍ő呬�x��ݒ�i�Œ�l�j
        VCamera.m_XAxis.m_MaxSpeed = MouseSensitivityMultiplier;

        // �I�[�f�B�I�~�L�T�[��BGM�̃{�����[�����X���C�_�[�ɐݒ�
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;

        // �I�[�f�B�I�~�L�T�[��SE�̃{�����[�����X���C�_�[�ɐݒ�
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    // BGM���ʂ�ݒ肷�郁�\�b�h
    public void SetBGM(float volume)
    {
        // �I�[�f�B�I�~�L�T�[��BGM�̉��ʂ�ݒ�
        audioMixer.SetFloat("BGM", volume);
    }

    // SE���ʂ�ݒ肷�郁�\�b�h
    public void SetSE(float volume)
    {
        // �I�[�f�B�I�~�L�T�[��SE�̉��ʂ�ݒ�
        audioMixer.SetFloat("SE", volume);
    }

    // �}�C�N���ʂ�ݒ肷�郁�\�b�h
    public void SetMic(float volume)
    {
        // �}�C�N��AudioSource�R���|�[�l���g���擾
        AudioSource Mic = micObject.GetComponent<AudioSource>();

        // �}�C�N�̉��ʂ��X���C�_�[�̒l�ɐݒ�
        Mic.volume = MicSlider.value;
    }

    // �}�E�X���x��ݒ肷�郁�\�b�h
    public void SetMouse(float level)
    {
        // VCamera��Y���̍ő呬�x���X���C�_�[�l����ɒ���
        VCamera.m_YAxis.m_MaxSpeed = level / MouseSensitivityDivisor;

        // VCamera��X���̍ő呬�x���X���C�_�[�l����ɒ���
        VCamera.m_XAxis.m_MaxSpeed = level * MouseSensitivityMultiplier;
    }
}