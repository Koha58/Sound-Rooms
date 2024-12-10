using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public delegate void RecordingStatusHandler(bool isRecording);
    public static event RecordingStatusHandler OnRecordingStatusChanged;

    public AudioSource audioSource;
    public int recordingDuration = 20;

    private string microphoneDevice;
    public bool isRecording = false;
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float nowdB;  // 録音中のdBを保持
    public MicAudioSource micAudioSource;
    private ParticleSystem RecordParticle;

    public bool itemDrop;

    void Start()
    {
        // MicAudioSourceの取得
        micAudioSource = FindObjectOfType<MicAudioSource>();

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
        isRecording = true;
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        recordingStartTime = Time.time;
        Debug.Log("録音を開始しました");
        RecordParticle.Play();

        // イベント通知: 録音が開始された
        OnRecordingStatusChanged?.Invoke(true);
    }

    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();
        Debug.Log("録音を停止し、録音した音声を再生します");
        RecordParticle.Stop();


        // イベント通知: 録音が停止された
        OnRecordingStatusChanged?.Invoke(false);

        recordedClip = null;
        Debug.Log("録音データを破棄しました");
    }

    public bool IsRecording()
    {
        return isRecording;
    }

    // UIクリック検知
    public bool IsPointerOverUI()
    {
        itemDrop = true;
        return EventSystem.current.IsPointerOverGameObject();
    }
}
