using UnityEngine;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public delegate void RecordingStatusHandler(bool isRecording);
    public static event RecordingStatusHandler OnRecordingStatusChanged;

    public AudioSource audioSource;
    public int recordingDuration = 20;

    private string microphoneDevice;
    private bool isRecording = false;
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float nowdB;  // �^������dB��ێ�
    private MicAudioSource micAudioSource;
    private ParticleSystem RecordParticle;

    void Start()
    {
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        micAudioSource = GetComponent<MicAudioSource>();
        GameObject RecordEffect = GameObject.Find("RecordParticle");
        RecordParticle = RecordEffect.GetComponent<ParticleSystem>();
        RecordParticle.Stop();
    }

    void Update()
    {
        if (isRecording && Time.time - recordingStartTime >= recordingDuration)
        {
            StopRecording();
        }

        if (micAudioSource != null)
        {
            nowdB = micAudioSource.now_dB;
        }
    }

    public void OnRecordButtonClicked()
    {
        if (isRecording || microphoneDevice == null) return;
        StartRecording();
    }

    public void StartRecording()
    {
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        isRecording = true;
        recordingStartTime = Time.time;
        Debug.Log("�^�����J�n���܂���");
        RecordParticle.Play();

        // �C�x���g�ʒm: �^�����J�n���ꂽ
        OnRecordingStatusChanged?.Invoke(true);
    }

    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();
        Debug.Log("�^�����~���A�^�������������Đ����܂�");
        RecordParticle.Stop();

        // �C�x���g�ʒm: �^������~���ꂽ
        OnRecordingStatusChanged?.Invoke(false);

        recordedClip = null;
        Debug.Log("�^���f�[�^��j�����܂���");
    }

    public bool IsRecording()
    {
        return isRecording;
    }
}
