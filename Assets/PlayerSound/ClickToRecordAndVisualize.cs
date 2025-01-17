using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public delegate void RecordingStatusHandler(bool isRecording);
    public static event RecordingStatusHandler OnRecordingStatusChanged;

    public AudioSource audioSource;
    public int recordingDuration = 20;  // �^�����ԁi�b�j

    private string microphoneDevice;
    public bool isRecording = false;  // �^�������ǂ���
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float currentDB;  // �^������dB�i���ʁj��ێ�
    public MicAudioSource micAudioSource;
    private ParticleSystem recordEffectParticle;

    public float recordingTime;

    private float stopRecordingTime = -1f;  // �^����~���UI�������܂ł̎���

    void Start()
    {
        // �^���J�n���� (�V�[���J�n������^�����J�n)
        micAudioSource = FindObjectOfType<MicAudioSource>();
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        recordEffectParticle = GameObject.Find("RecordParticle").GetComponent<ParticleSystem>();
        recordEffectParticle.Stop();
        recordingTime = 0;

        // �V�[���J�n���ɘ^�����J�n�i�^�������ۂɍs����킯�ł͂Ȃ��A�o�b�N�O���E���h�ōs����j
        StartRecording();
    }

    void Update()
    {
        if (isRecording && Time.time - recordingStartTime >= recordingDuration)
        {
            StopRecording();
        }

        if (micAudioSource != null)
        {
            currentDB = micAudioSource.now_dB;
        }

        // �^����~��A10�b���UI���\���ɂ��čĐ����J�n
        if (stopRecordingTime > 0 && Time.time >= stopRecordingTime + 10f)
        {
            HideRecordingUI();
            PlayRecordedAudio();
        }
    }

    // �^���J�n����
    public void OnRecordButtonClicked()
    {
        if (isRecording || microphoneDevice == null) return;

        // �^���J�n�{�^���������ꂽ�^�C�~���O��UI��\��
        ShowRecordingUI();

        // ���ۂɘ^�����n�܂�
        StartRecording();
    }

    // �^���J�n
    private void StartRecording()
    {
        if (isRecording || microphoneDevice == null) return;

        isRecording = true;
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        recordingStartTime = Time.time;
        Debug.Log("�^�����J�n���܂���");
        recordEffectParticle.Play();  // �^���G�t�F�N�g���Đ�

        OnRecordingStatusChanged?.Invoke(true);
    }

    // �^����~
    private void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();  // �^�������������Đ�
        Debug.Log("�^�����~���A�^�������������Đ����܂�");
        recordEffectParticle.Stop();  // �^���G�t�F�N�g���~

        OnRecordingStatusChanged?.Invoke(false);

        stopRecordingTime = Time.time;  // �^����~���Ԃ��L�^
        recordedClip = null;  // �^���f�[�^��j��
        Debug.Log("�^���f�[�^��j�����܂���");
    }

    // UI�̕\��
    private void ShowRecordingUI()
    {
        OnRecordingStatusChanged?.Invoke(true);  // UI��\��
    }

    // UI�̔�\��
    private void HideRecordingUI()
    {
        OnRecordingStatusChanged?.Invoke(false);  // UI���\��
    }

    // �^�������̍Đ�
    private void PlayRecordedAudio()
    {
        if (recordedClip != null)
        {
            audioSource.PlayOneShot(recordedClip);  // �^�����ꂽ�������Đ�
            Debug.Log("�^���������Đ����܂�");
        }
    }
}
