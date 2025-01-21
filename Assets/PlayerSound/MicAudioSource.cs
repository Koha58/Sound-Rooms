using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MicAudioSource : MonoBehaviour
{
    //サンプリング周波数
    static readonly int SAMPLE_RATE = 48000;

    //この秒数の幅で振幅の平均値を取ったものでdB値を更新
    static readonly float MOVING_AVE_TIME = 0.05f;

    //MOVING_AVE_TIMEに相当するサンプル数
    static readonly int MOVING_AVE_SAMPLE = (int)(SAMPLE_RATE * MOVING_AVE_TIME);

    //マイクのClipをセットする為のAudioSource
    AudioSource micAS = null;

    //現在のdB値
    private float _now_dB;
    public float now_dB { get { return _now_dB; } }

    private void Awake()
    {
        //AudioSourceコンポーネント取得
        micAS = GetComponent<AudioSource>();
    }

    void Start()
    {
        //最初にマイクの入力を開始
        this.MicStart();
    }

    // マイク入力の開始
    public void MicStart()
    {
        // AudioSourceのClipにマイクデバイスをセット
        micAS.clip = Microphone.Start(null, true, 1, SAMPLE_RATE);

        // マイクデバイスの準備ができるまで待つ
        while (!(Microphone.GetPosition("") > 0)) { }

        // AudioSourceからの出力を開始
        micAS.Play();
    }

    // MicAudioSource クラス
    void Update()
    {
        if (micAS.isPlaying)
        {
            // GetOutputData 用のバッファを準備
            float[] data = new float[MOVING_AVE_SAMPLE];

            // AudioSource から出力されているサンプルを取得
            micAS.GetOutputData(data, 0);

            // バッファ内の平均振幅を取得（絶対値を平均する）
            float aveAmp = data.Average(s => Mathf.Abs(s));

            // 振幅を dB（デシベル）に変換
            float dB = 20.0f * Mathf.Log10(aveAmp);

            // 現在値（now_dB）を更新
            _now_dB = dB;
        }
    }
}
