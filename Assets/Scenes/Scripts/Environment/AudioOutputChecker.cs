using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スピーカーが接続されていない場合にエラーUIを表示するクラス
/// 可聴域の小音量トーンを再生し、AudioListener から出力をチェックする
/// </summary>
public class AudioOutputChecker : MonoBehaviour
{
    [SerializeField] private GameObject SpeakerConnectionBadUI; // スピーカー未接続時に表示するUIオブジェクト

    // 定数定義
    private const int SampleRate = 44100;              // 音声サンプルのサンプルレート（Hz）
    private const float ToneFrequency = 440f;          // テスト用のトーン周波数（440Hzはラの音）
    private const float SourceVolume = 0.001f;          // AudioSourceの再生音量（非常に小さい音量）
    private const int SamplesLength = SampleRate;       // 1秒間分のサンプル数
    private const float CheckInterval = 3f;             // スピーカー出力チェックの間隔（秒）
    private const float OutputThreshold = 0.0001f;      // 出力音レベルの判定閾値（これ以下ならスピーカー未接続と判定）

    private float[] audioSamples = new float[256];      // 出力音声を取得するためのバッファ（256サンプル）
    private float timer = 0f;                            // チェック用タイマー

    private AudioSource source;                          // 音声再生用のAudioSourceコンポーネント

    void Start()
    {
        SetSpeakerUIVisible(false);                      // 初期状態でエラーUIを非表示にする
        PlayLowVolumeTone();                             // 小音量のテストトーンを再生開始
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= CheckInterval)
        {
            timer = 0f;
            CheckSpeakerOutput();                        // 定期的にスピーカー出力の有無をチェック
        }
    }

    /// <summary>
    /// 可聴域のテストトーン（440Hz）を1秒分生成して小音量で再生する
    /// </summary>
    private void PlayLowVolumeTone()
    {
        float[] samples = new float[SamplesLength];

        // 440Hzのサイン波を1秒分生成
        for (int i = 0; i < SamplesLength; i++)
        {
            samples[i] = Mathf.Sin(2 * Mathf.PI * ToneFrequency * i / SampleRate);
        }

        // AudioClip作成、サンプルをセット
        AudioClip clip = AudioClip.Create("LowVolumeTone", SamplesLength, 1, SampleRate, false);
        clip.SetData(samples, 0);

        // AudioSource追加・設定
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;             // ループ再生
        source.volume = SourceVolume;   // 非常に小さい音量
        source.Play();
    }

    /// <summary>
    /// AudioListenerから出力音声を取得し、スピーカーの接続有無を判定する
    /// </summary>
    private void CheckSpeakerOutput()
    {
        // チャンネル0から256サンプルを取得
        AudioListener.GetOutputData(audioSamples, 0);

        // 音量レベル計算（絶対値の合計）
        float level = 0f;
        foreach (var sample in audioSamples)
        {
            level += Mathf.Abs(sample);
        }

        // 音量が閾値より小さいならスピーカー未接続と判定
        bool noOutput = level < OutputThreshold;

        // UIの表示・非表示切り替え
        SetSpeakerUIVisible(noOutput);

        // ログ出力
        if (noOutput)
        {
            Debug.LogWarning("スピーカーが接続されていない可能性があります");
        }
        else
        {
            Debug.Log("スピーカーが接続されています");
        }
    }

    /// <summary>
    /// スピーカー接続エラーUIの表示・非表示を切り替える
    /// </summary>
    /// <param name="isVisible">表示するならtrue、非表示ならfalse</param>
    private void SetSpeakerUIVisible(bool isVisible)
    {
        if (SpeakerConnectionBadUI != null)
        {
            var image = SpeakerConnectionBadUI.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = isVisible;
            }
        }
    }
}
