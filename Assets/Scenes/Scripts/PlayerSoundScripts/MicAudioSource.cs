using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// マイクの音声入力を管理し、リアルタイムの音量（dB）を取得するクラス。
/// マイクが接続されていない場合は自動的に無効化され、クラッシュを防止する。
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MicAudioSource : MonoBehaviour
{
    // サンプリングレート（48kHz）
    static readonly int SAMPLE_RATE = 48000;

    // dB更新のための時間幅（秒）
    static readonly float MOVING_AVE_TIME = 0.05f;

    // dB更新に必要なサンプル数
    static readonly int MOVING_AVE_SAMPLE = (int)(SAMPLE_RATE * MOVING_AVE_TIME);

    // AudioSource（マイク音声再生用）
    AudioSource micAS = null;

    // 現在のdB値（外部から取得可能なプロパティ）
    private float _now_dB;
    // 現在のdB値（マイクが再生中でなければ -80dB を返す）
    public float now_dB
    {
        get
        {
            // マイクが未接続または再生中でない場合は -80dB を返す（無音扱い）
            if (micAS == null || !micAS.isPlaying)
                return -80.0f;

            return _now_dB;
        }
    }

    /// <summary>
    /// 初期化処理。AudioSource を取得。
    /// </summary>
    private void Awake()
    {
        micAS = GetComponent<AudioSource>();
    }

    /// <summary>
    /// マイク入力を開始する。
    /// 接続されていない場合はスクリプトを無効化。
    /// </summary>
    void Start()
    {
        // マイクが接続されていない場合はエラーログを出してスクリプトを停止
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("マイクが接続されていません。MicAudioSource は無効化されます。");
            enabled = false;  // Updateなども止める
            return;
        }

        // マイク入力を開始（コルーチンで非同期に開始）
        StartCoroutine(MicStartCoroutine());
    }

    /// <summary>
    /// マイク録音を非同期で開始する処理。
    /// マイク準備が整うまで待ってから再生を開始。
    /// </summary>
    IEnumerator MicStartCoroutine()
    {
        // デフォルトマイクから録音を開始（1秒ループ）
        micAS.clip = Microphone.Start(null, true, 1, SAMPLE_RATE);

        // マイクの録音準備が完了するまで待つ
        while (Microphone.GetPosition(null) <= 0)
        {
            yield return null;  // 次のフレームまで待機
        }

        // 再生開始（録音した音をそのまま再生）
        micAS.Play();
    }

    /// <summary>
    /// 毎フレーム、音声データを取得して現在の音量（dB）を計算
    /// </summary>
    void Update()
    {
        // AudioSource が再生中であれば音量データを処理
        if (micAS != null && micAS.isPlaying)
        {
            // サンプルバッファを準備
            float[] data = new float[MOVING_AVE_SAMPLE];

            // 現在再生されている音声のサンプルデータを取得
            micAS.GetOutputData(data, 0);

            // 絶対値の平均で振幅を算出
            float aveAmp = data.Average(s => Mathf.Abs(s));

            // 無音状態に近いときの下限値を設定（ログ計算エラー防止）
            if (aveAmp < 0.0001f)
            {
                aveAmp = 0.0001f;
            }

            // dBに変換（デシベル = 20 * log10(振幅)）
            _now_dB = 20.0f * Mathf.Log10(aveAmp);
        }
    }
}
