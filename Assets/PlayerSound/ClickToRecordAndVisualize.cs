using UnityEngine;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public AudioSource audioSource;
    public int recordingDuration = 10;

    private string microphoneDevice;
    private bool isRecording = false;
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float nowdB;  // �^������dB��ێ�

    private MicAudioSource micAudioSource;  // MicAudioSource���Ǘ����邽�߂̎Q��

    ParticleSystem RecordParticle;

    void Start()
    {
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        micAudioSource = GetComponent<MicAudioSource>(); // MicAudioSource�̎擾
        GameObject RecordEffect = GameObject.Find("RecordParticle");
        RecordParticle = RecordEffect.GetComponent<ParticleSystem>();
        RecordParticle.Stop();
    }

    void Update()
    {
        GameObject RecordEffect = GameObject.Find("RecordParticle");
        RecordParticle = RecordEffect.GetComponent<ParticleSystem>();

        // �^�����ɘ^�����Ԃ��o�߂������~
        if (isRecording && Time.time - recordingStartTime >= recordingDuration)
        {
            StopRecording();
        }

        // �^�����ł��^����ł��AdB����ɍX�V
        if (micAudioSource != null)
        {
            nowdB = micAudioSource.now_dB;
        }

        // �^������dB���X�V�i�^�����ɘ^���f�[�^���擾���鏈���j
        if (isRecording)
        {
            int position = Microphone.GetPosition(microphoneDevice);
            if (position > 0)
            {
                float[] samples = new float[position];
                recordedClip.GetData(samples, 0);
                nowdB = CalculateDB(samples);  // �^������dB���X�V
            }
        }
    }

    // dB�̌v�Z
    float CalculateDB(float[] samples)
    {
        float sum = 0;
        foreach (var sample in samples)
        {
            sum += Mathf.Abs(sample);
        }
        float rms = Mathf.Sqrt(sum / samples.Length);  // RMS (Root Mean Square)
        float db = 20 * Mathf.Log10(rms);  // dB�l�ɕϊ�
        return Mathf.Clamp(db, -60f, 0f);  // dB�l��͈͓��ɐ���
    }

    // �^���{�^�����N���b�N���ꂽ���̏���
    public void OnRecordButtonClicked()
    {
        if (isRecording || microphoneDevice == null) return;

        StartRecording();
    }

    // �^���J�n
    public void StartRecording()
    {
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        isRecording = true;
        recordingStartTime = Time.time;
        Debug.Log("�^�����J�n���܂���");
        RecordParticle.Play();
    }

    // �^����~
    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();
        Debug.Log("�^�����~���A�^�������������Đ����܂�");
        RecordParticle.Stop();

        // �^���f�[�^���Đ���ɔj��
        recordedClip = null;
        Debug.Log("�^���f�[�^��j�����܂���");
    }

    public bool IsRecording()
    {
        return isRecording;
    }
}
