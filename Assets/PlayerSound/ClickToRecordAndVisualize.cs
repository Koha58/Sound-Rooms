using UnityEngine;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public AudioSource audioSource;
    public int recordingDuration = 10;

    private string microphoneDevice;
    private bool isRecording = false;
    private AudioClip recordedClip;
    private float recordingStartTime;

    public float nowdB;  // 録音中のdBを保持

    private MicAudioSource micAudioSource;  // MicAudioSourceを管理するための参照

    ParticleSystem RecordParticle;

    void Start()
    {
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
        micAudioSource = GetComponent<MicAudioSource>(); // MicAudioSourceの取得
        GameObject RecordEffect = GameObject.Find("RecordParticle");
        RecordParticle = RecordEffect.GetComponent<ParticleSystem>();
        RecordParticle.Stop();
    }

    void Update()
    {
        GameObject RecordEffect = GameObject.Find("RecordParticle");
        RecordParticle = RecordEffect.GetComponent<ParticleSystem>();

        // 録音中に録音時間を経過したら停止
        if (isRecording && Time.time - recordingStartTime >= recordingDuration)
        {
            StopRecording();
        }

        // 録音中でも録音後でも、dBを常に更新
        if (micAudioSource != null)
        {
            nowdB = micAudioSource.now_dB;
        }

        // 録音中のdBを更新（録音中に録音データを取得する処理）
        if (isRecording)
        {
            int position = Microphone.GetPosition(microphoneDevice);
            if (position > 0)
            {
                float[] samples = new float[position];
                recordedClip.GetData(samples, 0);
                nowdB = CalculateDB(samples);  // 録音中のdBを更新
            }
        }
    }

    // dBの計算
    float CalculateDB(float[] samples)
    {
        float sum = 0;
        foreach (var sample in samples)
        {
            sum += Mathf.Abs(sample);
        }
        float rms = Mathf.Sqrt(sum / samples.Length);  // RMS (Root Mean Square)
        float db = 20 * Mathf.Log10(rms);  // dB値に変換
        return Mathf.Clamp(db, -60f, 0f);  // dB値を範囲内に制限
    }

    // 録音ボタンがクリックされた時の処理
    public void OnRecordButtonClicked()
    {
        if (isRecording || microphoneDevice == null) return;

        StartRecording();
    }

    // 録音開始
    public void StartRecording()
    {
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        isRecording = true;
        recordingStartTime = Time.time;
        Debug.Log("録音を開始しました");
        RecordParticle.Play();
    }

    // 録音停止
    void StopRecording()
    {
        if (!isRecording) return;

        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        audioSource.Play();
        Debug.Log("録音を停止し、録音した音声を再生します");
        RecordParticle.Stop();

        // 録音データを再生後に破棄
        recordedClip = null;
        Debug.Log("録音データを破棄しました");
    }

    public bool IsRecording()
    {
        return isRecording;
    }
}
