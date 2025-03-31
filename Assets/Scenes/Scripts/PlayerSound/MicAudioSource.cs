using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// マイクの入力を管理するクラス
/// 音量（dB）をリアルタイムで取得し、他のコンポーネントで利用できるようにする
/// </summary>
public class MicAudioSource : MonoBehaviour
{
    // サンプリング周波数 (48kHz)
    static readonly int SAMPLE_RATE = 48000;

    // この秒数の幅で振幅の平均値を取ったものでdB値を更新
    // 音量（dB）の変化をなめらかにするために一定時間ごとに更新する
    static readonly float MOVING_AVE_TIME = 0.05f;

    // MOVING_AVE_TIMEに相当するサンプル数
    static readonly int MOVING_AVE_SAMPLE = (int)(SAMPLE_RATE * MOVING_AVE_TIME);

    // マイクの音声を扱うためのAudioSourceコンポーネント
    AudioSource micAS = null;

    // 現在のdB値
    private float _now_dB;  // プライベートな変数で、外部アクセス用のプロパティを提供
    public float now_dB { get { return _now_dB; } }  // 外部から現在のdB値を取得するためのプロパティ


    private void Awake()
    {
        // AudioSourceコンポーネントを取得
        micAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 最初にマイクの入力を開始
        this.MicStart();
    }

    // マイク入力の開始
    public void MicStart()
    {
        // マイクデバイスをAudioSourceのClipにセット
        // Microphone.Startでマイクの録音を開始
        micAS.clip = Microphone.Start(null, true, 1, SAMPLE_RATE);

        // マイクデバイスの準備ができるまで待機（マイクから音が取得可能になるのを待つ）
        while (!(Microphone.GetPosition("") > 0)) { }

        // マイクの音声の再生を開始
        micAS.Play();
    }

    void Update()
    {
        if (micAS.isPlaying)  // マイクが再生中であれば
        {
            // GetOutputData用のバッファを準備
            // 音声データを取得するための配列
            float[] data = new float[MOVING_AVE_SAMPLE];

            // AudioSourceから出力されているサンプルデータを取得
            // ここで取得したデータは、マイクの入力音声の振幅情報
            micAS.GetOutputData(data, 0);

            // バッファ内の平均振幅を取得（各振幅の絶対値を取り平均値を計算）
            // 振幅の平均を使うことで、音量のピーク値ではなく平均的な音量を取得
            float aveAmp = data.Average(s => Mathf.Abs(s));

            // 振幅が非常に小さい（ゼロに近い）場合でも音を拾うため、最小振幅を設定
            if (aveAmp < 0.0001f) // ここで0.0001は非常に小さい音量の閾値
            {
                aveAmp = 0.0001f; // 振幅が非常に小さい場合でも0.0001の振幅に設定
            }

            // 平均振幅からdB（デシベル）に変換
            _now_dB = 20.0f * Mathf.Log10(aveAmp);

            // Debugログ（必要に応じて確認）
            Debug.Log(_now_dB);
        }
    }
}