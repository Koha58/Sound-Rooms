using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public delegate void RecordingStatusHandler(bool isRecording);
    public static event RecordingStatusHandler OnRecordingStatusChanged;

    public AudioSource audioSource;
    public int recordingDuration = 20;  // 録音時間（秒）

    private string microphoneDevice;
    public bool isRecording = false;  // 録音中かどうか
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float currentDB;  // 録音中のdB（音量）を保持
    public MicAudioSource micAudioSource;
    private ParticleSystem recordEffectParticle;

    public float recordingTime;

    private float stopRecordingTime = -1f;  // 録音停止後にUIを消すまでの時間

    void Start()
    {
        // 初期設定（録音ボタンを押したときに録音開始）
        micAudioSource = FindObjectOfType<MicAudioSource>();
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        recordEffectParticle = GameObject.Find("RecordParticle").GetComponent<ParticleSystem>();
        recordEffectParticle.Stop();
        recordingTime = 0;

        // ボタンを押したときに録音を開始するように設定
    }

    void Update()
    {
        if (isRecording && Time.time - recordingStartTime >= recordingDuration)
        {
            StopRecording();

            if (micAudioSource != null)
            {
                currentDB = micAudioSource.now_dB;
            }
        }

        // 録音停止後、10秒後にUIを非表示にして再生を開始
        if (stopRecordingTime > 0 && Time.time >= stopRecordingTime + 10f)
        {
            HideRecordingUI();
            PlayRecordedAudio();
        }
    }

    // 録音開始ボタンが押されたときに呼ばれるメソッド
    public void OnRecordButtonClicked()
    {
        if (isRecording || microphoneDevice == null) return;

        // 録音開始ボタンが押されたタイミングでUIを表示
        ShowRecordingUI();

        // 実際に録音が始まる
        StartRecording();
    }

    // 録音開始
    private void StartRecording()
    {
        if (isRecording || microphoneDevice == null) return;

        isRecording = true;
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);  // 録音開始
        recordingStartTime = Time.time;
        Debug.Log("録音を開始しました");
        recordEffectParticle.Play();  // 録音エフェクトを再生

        OnRecordingStatusChanged?.Invoke(true);
    }

    // 録音停止
    private void StopRecording()
    {
        if (!isRecording) return;

        // 録音を停止
        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();  // 録音した音声を再生
        Debug.Log("録音を停止し、録音した音声を再生します");
        recordEffectParticle.Stop();  // 録音エフェクトを停止

        // 録音停止後にMicAudioSourceに音声入力の権限を返す
        micAudioSource.MicStart(); // 録音後に再度マイクを有効化

        OnRecordingStatusChanged?.Invoke(false);

        stopRecordingTime = Time.time;  // 録音停止時間を記録
        recordedClip = null;  // 録音データを破棄
        Debug.Log("録音データを破棄しました");
    }

    // UIの表示
    private void ShowRecordingUI()
    {
        OnRecordingStatusChanged?.Invoke(true);  // UIを表示
    }

    // UIの非表示
    private void HideRecordingUI()
    {
        OnRecordingStatusChanged?.Invoke(false);  // UIを非表示
    }

    // 録音音声の再生
    private void PlayRecordedAudio()
    {
        if (recordedClip != null)
        {
            audioSource.PlayOneShot(recordedClip);  // 録音された音声を再生
            Debug.Log("録音音声を再生します");
        }
    }
}
